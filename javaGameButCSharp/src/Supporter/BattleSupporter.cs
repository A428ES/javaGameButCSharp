using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class BattleSupporter : ISupporter{
        private readonly SupporterContext _supporterContext;
        public BattleSupporter(SupporterContext supportContext){
            _supporterContext = supportContext;
        }
        public void Routing(OptionMap overrideInput = EVENT_COMPLETE){
            
        }
    }
}