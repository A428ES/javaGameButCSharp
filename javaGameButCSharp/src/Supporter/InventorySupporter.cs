using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class InventorySupporter(SupporterContext supporterContext) : Supporter(supporterContext){
    
        public List<string> ItemStringList(List<Item> list){
            return list.Select(obj => obj.Name).ToList();
        }

        public void ListItems(string type){
            List<string> itemList = ItemStringList(_supporterContext.GameState.ActivePlayer.Inventory.GetItemType(type));
            itemList.AddRange(GlobalMenuOptions(["MEDICINE", "WEAPONS", "ARMOR", "STATS"]));

            _supporterContext.IO.OutWithOptionsPrompt("SELECT AN ITEM",itemList);

            if(itemList.Contains(_supporterContext.IO.LastUserInput.ToUpper())){
                _supporterContext.SystemEvent = new(ITEM_EVENT);
            }
        }
        public override List<string> FinalOptionsProcessing(){
            return GlobalMenuOptions(["INVENTORY"]);
        }

        public override Dictionary<OptionMap, Action> MapRoute(){
            return new Dictionary<OptionMap, Action>          
                {
                    {MEDICINE, () => ListItems("MEDICINE")},
                    {ARMORS, () => ListItems("ARMOR")},
                    {WEAPONS, () => ListItems("WEAPON")},
                };
        }

        
    }

}