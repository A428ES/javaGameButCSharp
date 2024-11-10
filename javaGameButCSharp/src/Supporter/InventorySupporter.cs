using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class InventorySupporter(SupporterContext supporterContext) : Supporter(supporterContext){
    
        public List<string> ItemStringList(List<Item> list){
            return list.Select(obj => obj.Name).ToList();
        }

        public void ListItems(string type){
            List<string> medicineList = ItemStringList(_supporterContext.GameState.ActivePlayer.Inventory.GetItemType(type));
            medicineList.Add("EXIT");

            _supporterContext.IO.OutWithOptionsPrompt("SELECT AN ITEM",medicineList);

            if(medicineList.Contains(_supporterContext.IO.LastUserInput)){
                _supporterContext.SystemEvent = new(ITEM_EVENT);
            }
        }
        public void Resume(){
            _supporterContext.ResumeGame();
        }

        public override Dictionary<OptionMap, Action> MapRoute(){
            return new Dictionary<OptionMap, Action>          
                {
                    {MEDICINE, () => ListItems("MEDICINE")},
                    {ARMORS, () => ListItems("ARMOR")},
                    {WEAPONS, () => ListItems("WEAPON")},
                    {RESUME, () => Resume()}
                };
        }

        
    }

}