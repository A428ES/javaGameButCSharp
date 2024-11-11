using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class LocationSupporter(SupporterContext supporterContext) : Supporter(supporterContext) {
        private const string _LocationText = "HEADS UP";

        public override Dictionary<String, String> StageKeywordReplace(){
            var activeLocation = _supporterContext.GameState.ActiveLocation;

            return new Dictionary<string, string>
                    {
                        {"{target}", activeLocation.Name},
                        {"{targetDescription}", activeLocation.Description},
                        {"{npcList}", $"[{string.Join(", ", activeLocation.NpcList)}]" }
                    };
        }

        public void DirectionalMoveAttempt(string newLocation){
            _supporterContext.RetryHelper.ExecuteWithRetry(
                        () => DirectionalMove(newLocation), 
                        2,
                        () => DirectionalMove(_supporterContext.GameState.ActiveLocation.Name, true)
                    );
        }

        public void DirectionalMove(string location, bool retryDialog=false){
            if(retryDialog){
                _supporterContext.IO.OutWithSubject(_LocationText, $"In your search, you find nothing. You begin your journey back to {location}");
            }

            _supporterContext.SystemEvent = new EventModel(LOCATION_EVENT, location);
            _supporterContext.GameState.UpdateLocation(location);
        }

        public void BadInput(){
            _supporterContext.IO.OutWithSubject(_LocationText, "Invalid action");
        }

        public bool CheckNPCInteractAttempt(){
            if(_supporterContext.GameState.ActiveLocation.NpcList.Contains(_supporterContext.IO.LastUserInput.ToUpper())){
                _supporterContext.SystemEvent = new(ENTITY_EVENT, _supporterContext.IO.LastUserInput);
                return true;
            }

            return false;
        }

        public override List<string> FinalOptionsProcessing(){
            var options = GlobalMenuOptions(["RESUME"]);

            options.AddRange(_supporterContext.GameState.ActiveLocation.NpcList);

            return options;
        }

        public override Dictionary<OptionMap, Action> MapRoute() {
            return new Dictionary<OptionMap, Action>
                {
                    {EAST, ()=>DirectionalMoveAttempt(_supporterContext.GameState.ActiveLocation.PreviousLocation)},
                    {WEST, ()=>DirectionalMoveAttempt(_supporterContext.GameState.ActiveLocation.NextLocation)},
                    {ALTERNATIVE, () => CheckNPCInteractAttempt()}
                };
        }
    }
}