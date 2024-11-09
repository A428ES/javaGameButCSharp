using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class BattleSupporter : ISupporter{
        private readonly SupporterContext _supporterContext;
        public BattleSupporter(SupporterContext supportContext){
            _supporterContext = supportContext;
        }

        public Dictionary<String, String> StageKeywordReplace(){
            return new Dictionary<string, string>
                    {
                        {"{target}", _supporterContext.GameState.ActiveTargetNPC.Name},
                    };
        }

        public void Escape(){
            _supporterContext.SystemEvent = new(LOCATION_EVENT, _supporterContext.GameState.ActiveLocation.Name);
        }

        public void Routing(){
            OptionMap enumResult;

            _supporterContext.GameState.LoadNPCTarget(_supporterContext.IO.LastUserInput);
            _supporterContext.IO.OutWithKeyWordReplaceAndOptions(_supporterContext.WorkingEvent.EventText, StageKeywordReplace(),_supporterContext.WorkingEvent.InputOptions);
            Enum.TryParse<OptionMap>(_supporterContext.IO.LastUserInput, true, out enumResult);

            switch(enumResult){
                case ATTACK:
                    Escape();
                break;
                case ESCAPE:
                    Escape();
                break;
                case ITEM:
                    Escape();
                break;
                case MEDICINE:
                    Escape();
                break;
                default:
                    Console.WriteLine("test");
                break;
            }
        }
    }
}