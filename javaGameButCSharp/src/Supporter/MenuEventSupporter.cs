using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class MainMenuEventSupporter(SupporterContext supporterContext) : ISupporter{
        private readonly SupporterContext _supporterContext = supporterContext;
        public void NewGame(OptionMap enumResult){
            _supporterContext.IO.OutWithPrompt("CREATING NEW SAVE", "ENTER YOUR NAME");
            
            try{
                _supporterContext.GameState.NewPlayer(_supporterContext.IO.LastUserInput);
                LoadPlayerLocation();
            } catch (InvalidInput)
            {
                _supporterContext.IO.OutWithSubject("ERROR", "This save already exists!");

                Routing(enumResult);
            }
        }

        public void Stats(){
            _supporterContext.IO.OutWithSubject("GAME ENGINE","Loading stats ...");

            _supporterContext.GameState.LoadStats();
        }

        public void LoadPlayerLocation(){
            _supporterContext.SystemEvent = new EventModel(LOCATION_EVENT, _supporterContext.GameState.ActivePlayer.Location);
        }

        public void LoadGame(){
            _supporterContext.IO.OutWithPrompt("LOADING EXISTING GAME", "ENTER YOUR SAVE NAME");
            _supporterContext.GameState.LoadPlayer(_supporterContext.IO.LastUserInput);
            LoadPlayerLocation();
        }

        public void ExitGame(){
            _supporterContext.IO.OutWithSubject("GAME ENGINE", "Attempting shut down ...");
            _supporterContext.SystemEvent = new EventModel(EXIT);
        }
        
        public void Routing(OptionMap overrideInput = EVENT_COMPLETE) {
            EventModel nextEvent;
            OptionMap enumResult = overrideInput;

            if(overrideInput == EVENT_COMPLETE){
                _supporterContext.IO.OutWithOptionsPrompt(_supporterContext.WorkingEvent.EventText, _supporterContext.WorkingEvent.InputOptions);

                if(!Enum.TryParse<OptionMap>(_supporterContext.IO.LastUserInput, true, out enumResult)){
                    throw new InvalidInput("Not a valid game engine input");
                }
            }

            switch(enumResult){
                case EXIT:
                    ExitGame();
                break;
                case NEW:
                    NewGame(enumResult);
                break;
                case LOAD:
                    LoadGame();
                break;
                case STATS:
                    Stats();
                break;
                default:
                    nextEvent = new EventModel(LOCATION_EVENT, "tatooine");
                break;
            }
        }
    }
}