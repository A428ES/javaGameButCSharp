namespace JavaGameButCSharp{
    class BattleSupporter(SupporterContext supporterContext) : Supporter(supporterContext){
        public override Dictionary<OptionMap, Action> MapRoute(){
            return new Dictionary<OptionMap, Action>{};
        }
    }
}