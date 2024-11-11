using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EntitySupporter(SupporterContext supporterContext) : Supporter(supporterContext){

        public override Dictionary<String, String> StageKeywordReplace(){
            return new Dictionary<string, string>
                    {
                        {"{target}", _supporterContext.GameState.ActiveTargetNPC.Name},
                    };
        }

        public void Battle(){
            if(_supporterContext.GameState.ActivePlayer.Health <= 0){
                _supporterContext.IO.OutWithSubject("HEADS UP", "You can't battle with no health!");
                
                Leave();

                return;
            }
            
            _supporterContext.ToggleBattleOn();
            _supporterContext.SystemEvent = new(BATTLE_EVENT, _supporterContext.GameState.ActiveTargetNPC.Name);
        }

        public void Leave(){
            _supporterContext.SystemEvent = new(LOCATION_EVENT, _supporterContext.GameState.ActiveLocation.Name);
        }

        public override List<string> FinalOptionsProcessing()
        {
            return GlobalMenuOptions(["RESUME"]);
        }

        public override Dictionary<OptionMap, Action> MapRoute() {
            _supporterContext.GameState.LoadNPCTarget(_supporterContext.IO.LastUserInput);
            
            return new Dictionary<OptionMap, Action>
                {
                    {BATTLE, () => Battle()},
                    {LEAVE, () => Leave()},
                };
        }
    }
}