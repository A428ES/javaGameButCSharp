namespace JavaGameButCSharp{
    abstract class Supporter{
        public SupporterContext _supporterContext {get;set;}

        public abstract Dictionary<OptionMap, Action> MapRoute();

        public Supporter(SupporterContext supporterContext){
            _supporterContext = supporterContext;
        }

        public void RetryWorker(Action action, int maxRetries=1){
            _supporterContext.RetryHelper.ExecuteWithRetry(action, maxRetries);
        }

        public virtual Dictionary<String, String> StageKeywordReplace(){
            return new(){};
        }

        public virtual List<string> FinalOptionsProcessing(){
            return _supporterContext.WorkingEvent.InputOptions;
        }

        public List<string> GlobalMenuOptions(List<string> exclude = null){
            var options = new List<string>();
            exclude ??= [];

            if (!string.IsNullOrEmpty(_supporterContext.GameState.ActivePlayer.Name)){
                options.Add("RESUME");
                options.Add("INVENTORY");
                options.Add("STATS");
            } 
            options.Add("MENU");
            options.AddRange(_supporterContext.WorkingEvent.InputOptions);

            return options.Where(option => !exclude.Contains(option)).ToList();
        }

        public Dictionary<OptionMap, Action> GlobalRoutes(Dictionary<OptionMap, Action> routes){
            routes.Add(OptionMap.RESUME, () => _supporterContext.ResumeGame());
            routes.Add(OptionMap.STATS, () => _supporterContext.GameState.LoadStats());
            routes.Add(OptionMap.MENU, () => _supporterContext.ResetToMenu());
            routes.Add(OptionMap.INVENTORY, () => _supporterContext.Inventory());

            return routes;
        }

        public void Routing(Dictionary<OptionMap, Action> supporterMap){
            _supporterContext.IO.OutWithKeyWordReplaceAndOptions(_supporterContext.WorkingEvent.EventText, StageKeywordReplace(),FinalOptionsProcessing());

            supporterMap = GlobalRoutes(supporterMap);

            Enum.TryParse<OptionMap>(_supporterContext.IO.LastUserInput, true, out OptionMap enumResult);

            Action? toRun = supporterMap.GetValueOrDefault(enumResult);
            toRun ??= supporterMap.GetValueOrDefault(OptionMap.ALTERNATIVE);

            toRun?.Invoke();
        }
    }
}