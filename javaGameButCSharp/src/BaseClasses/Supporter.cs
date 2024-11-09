namespace JavaGameButCSharp{
    abstract class Supporter{
        public SupporterContext _supporterContext {get;set;}

        public abstract Dictionary<OptionMap, Action> MapRoute();

        public Supporter(SupporterContext supporterContext){
            _supporterContext = supporterContext;
        }

        public void RetryWorker(Action action, int maxRetries=5){
            _supporterContext.RetryHelper.ExecuteWithRetry(action, maxRetries);
        }

        public virtual Dictionary<String, String> StageKeywordReplace(){
            return new(){};
        }

        public virtual List<string> FinalOptionsProcessing(){
            return _supporterContext.WorkingEvent.InputOptions;
        }

        public void Routing(Dictionary<OptionMap, Action> supporterMap){
            _supporterContext.IO.OutWithKeyWordReplaceAndOptions(_supporterContext.WorkingEvent.EventText, StageKeywordReplace(),FinalOptionsProcessing());

            Enum.TryParse<OptionMap>(_supporterContext.IO.LastUserInput, true, out OptionMap enumResult);

            Action? toRun = supporterMap.GetValueOrDefault(enumResult);
            toRun ??= supporterMap.GetValueOrDefault(OptionMap.ALTERNATIVE);

            if(toRun != null){
                toRun();
            } else {
                throw new InvalidInput("This shouldn't have happened.");
            }
        }
    }
}