using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class ItemSupporter : ISupporter{
        private readonly SupporterContext _supportContext;
        public ItemSupporter(SupporterContext supportContext){
            _supportContext = supportContext;
        }
        public void Routing(){

        }
    }

}