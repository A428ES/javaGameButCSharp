using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EntitySupporter(SupporterContext supporterContext) : Supporter(supporterContext){

        public override Dictionary<String, String> StageKeywordReplace(){
            return new Dictionary<string, string>
                    {
                        {"{target}", _supporterContext.GameState.ActiveTargetNPC.Name},
                    };
        }

        public void Attack(){
            BattleSupporter battleEvent = new(_supporterContext);
        }

        public void Inventory(){
            InventorySupporter itemEvent = new(_supporterContext);
        }

        public void Escape(){
            _supporterContext.SystemEvent = new(LOCATION_EVENT, _supporterContext.GameState.ActiveLocation.Name);
        }

        public override Dictionary<OptionMap, Action> MapRoute() {
            _supporterContext.GameState.LoadNPCTarget(_supporterContext.IO.LastUserInput);
            
            return new Dictionary<OptionMap, Action>
                {
                    {ATTACK, ()=>Attack()},
                    {INVENTORY, ()=>Inventory()},
                    {ESCAPE, () => Escape()},
                };
        }
    }
}