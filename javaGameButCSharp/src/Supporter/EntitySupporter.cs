using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class BattleSupporter : ISupporter{
        private readonly SupporterContext _supporterContext;
        public BattleSupporter(SupporterContext supportContext){
            _supporterContext = supportContext;
        }

        public Dictionary<String, String> StageKeywordReplace(){
            var activeNPC = _supporterContext.GameState.ActiveTargetNPC;

            return new Dictionary<string, string>
                    {
                        {"{target}", activeNPC.Name},
                    };
        }

        public void Routing(OptionMap overrideInput = EVENT_COMPLETE){
            OptionMap enumResult = overrideInput;

            _supporterContext.GameState.LoadNPCTarget(_supporterContext.IO.LastUserInput);

            if(overrideInput == EVENT_COMPLETE){
                _supporterContext.IO.OutWithKeyWordReplaceAndOptions(_supporterContext.WorkingEvent.EventText, StageKeywordReplace(),_supporterContext.WorkingEvent.InputOptions);

                if(!Enum.TryParse<OptionMap>(_supporterContext.IO.LastUserInput, true, out enumResult)){
                    throw new InvalidInput("Not a valid game engine input");
                }
            }
        }
    }
}