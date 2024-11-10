using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class ItemSupporter(SupporterContext supporterContext) : Supporter(supporterContext){
        public void Activate(){

        }

        public void Drop(){

        }

        public void Resume(){
            _supporterContext.ResumeGame();
        }

        public override Dictionary<String, String> StageKeywordReplace(){
            var activeItem = _supporterContext.GameState.ActiveItem;

            return new Dictionary<string, string>
                    {
                        {"{item_name}", activeItem.Name},
                        {"{item_description}", activeItem.Description},
                    };
        }

        public override Dictionary<OptionMap, Action> MapRoute(){
            Item loadedItem = _supporterContext.GameState.ActivePlayer.Inventory.GetItem(_supporterContext.IO.LastUserInput);

            _supporterContext.GameState.LoadItem(loadedItem);

            return new Dictionary<OptionMap, Action>          
                {
                    {ACTIVATE, () => Activate()},
                    {DROP, () => Drop()},
                    {RESUME, () => Resume()}
                };
        }

    }
}