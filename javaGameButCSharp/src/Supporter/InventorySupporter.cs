using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class InventorySupporter(SupporterContext supporterContext) : Supporter(supporterContext){
    
        public override Dictionary<OptionMap, Action> MapRoute(){
            return new ();
        }
    }

}