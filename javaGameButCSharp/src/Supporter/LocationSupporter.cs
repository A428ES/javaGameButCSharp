using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class LocationSupporter : ISupporter{
        private readonly SupporterContext _supporterContext;

        public LocationSupporter(SupporterContext supportContext){
            _supporterContext = supportContext;
        }
        public void Routing(OptionMap overrideInput = EVENT_COMPLETE){

        }
    }
}