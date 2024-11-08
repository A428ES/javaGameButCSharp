using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class LocationSupporter : ISupporter{
        private readonly SupporterContext _supporterContext;
        private const string _LocationText = "HEADS UP";
        public LocationSupporter(SupporterContext supportContext){
            _supporterContext = supportContext;
        }

        public Dictionary<String, String> StageKeywordReplace(){
            var activeLocation = _supporterContext.GameState.ActiveLocation;

            return new Dictionary<string, string>
                    {
                        {"{target}", activeLocation.Name},
                        {"{targetDescription}", activeLocation.Description},
                        {"{npcList}", $"[{string.Join(", ", activeLocation.NpcList)}]" }
                    };
        }

        public void DirectionalMoveAttempt(string newLocation){
            RetryHelper.ExecuteWithRetry(
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
            if(_supporterContext.GameState.ActiveLocation.NpcList.Contains(_supporterContext.IO.LastUserInput)){
                _supporterContext.SystemEvent = new(ENTITY_EVENT, _supporterContext.IO.LastUserInput);
                return true;
            }

            return false;
        }

        public void Routing(OptionMap overrideInput = EVENT_COMPLETE){
            OptionMap enumResult = overrideInput;
        
            _supporterContext.WorkingEvent.InputOptions.AddRange(_supporterContext.GameState.ActiveLocation.NpcList);

            if(overrideInput == EVENT_COMPLETE){
                _supporterContext.IO.OutWithKeyWordReplaceAndOptions(_supporterContext.WorkingEvent.EventText, StageKeywordReplace(),_supporterContext.WorkingEvent.InputOptions);

                if(CheckNPCInteractAttempt()){
                    return;
                }

                if(!Enum.TryParse<OptionMap>(_supporterContext.IO.LastUserInput, true, out enumResult)){
                    throw new InvalidInput("Not a valid game engine input");
                }
            }

            switch(enumResult){
                case EAST:
                    DirectionalMoveAttempt(_supporterContext.GameState.ActiveLocation.PreviousLocation);
                    break;
                case WEST:
                    DirectionalMoveAttempt(_supporterContext.GameState.ActiveLocation.NextLocation);
                    break;
                case MENU:
                    _supporterContext.ResetToMenu();
                    break;
                default:
                    BadInput();
                    break;

            }

        }
    }
}