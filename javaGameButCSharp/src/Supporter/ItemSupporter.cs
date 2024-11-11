using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class ItemSupporter(SupporterContext supporterContext) : Supporter(supporterContext){
        private Item _itemInUse {get;set;} = new();
        private Entity _playerInUse {get;set;} = new();

        public void UseMedicine(){
            if(_playerInUse.Health == 100){
                _supporterContext.IO.OutWithSubject("FAILED", "It would have no effect");
                return;
            }

            _playerInUse.Health += _itemInUse.Modifier;
            _itemInUse.Quantity -= 1;


            if(_playerInUse.Health > 100){
                _playerInUse.Health = 100;
            }

            _supporterContext.IO.OutWithSubject("SUCCESS", $"YOU HEALED {_itemInUse.Modifier} HP using 1 {_itemInUse.Name}. You have {_itemInUse.Quantity} left. Your health is {_playerInUse.Health}");           
        }

        public void EquipWeapon(){
            _playerInUse.Inventory.ActiveWeapon = _itemInUse.Name;

            _supporterContext.IO.OutWithSubject("SUCCESS", $"YOU EQUIPPED {_itemInUse.Name}");       
        }

        public void EquipArmor(){
            _playerInUse.Inventory.ActiveArmor = _itemInUse.Name;

            _supporterContext.IO.OutWithSubject("SUCCESS", $"YOU EQUIPPED {_itemInUse.Name}");   
        }

        public void Activate(){
            if(_itemInUse.Type.Equals("MEDICINE")){
                UseMedicine();
            }

            if(_itemInUse.Type.Equals("ARMOR")){
                EquipArmor();
            }

            if(_itemInUse.Type.Equals("WEAPON")){
                EquipWeapon();
            }
            
            _supporterContext.GameState.SavePlayer();
            _supporterContext.SystemEvent = new(INVENTORY_EVENT, "");     
        }

        public override Dictionary<String, String> StageKeywordReplace(){
            var activeItem = _supporterContext.GameState.ActiveItem;

            return new Dictionary<string, string>
                    {
                        {"{item_name}", activeItem.Name},
                        {"{item_description}", activeItem.Description},
                    };
        }

        public override List<string> FinalOptionsProcessing(){
            return GlobalMenuOptions(["STATS"]);
        }

        public override Dictionary<OptionMap, Action> MapRoute(){
            _itemInUse = _supporterContext.GameState.ActivePlayer.Inventory.GetItem(_supporterContext.IO.LastUserInput);
            _playerInUse = _supporterContext.GameState.ActivePlayer;

            _supporterContext.GameState.LoadItem(_itemInUse);

            return new Dictionary<OptionMap, Action>          
                {
                    {ACTIVATE, () => Activate()},
                };
        }

    }
}