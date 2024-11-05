using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class MainMenuEventSupporter{
        private readonly InputOutManager _IO;
        private readonly GameStateController _gameState;
        private Event _workingEvent;
        public EventModel SystemEvent {get;set;} = new EventModel(MENU_EVENT);

        public MainMenuEventSupporter(Event workingEvent, InputOutManager io, GameStateController gameState){
            _IO = io;
            _workingEvent = workingEvent;
            _gameState = gameState;
        }

        public void NewGame(OptionMap enumResult){
            _IO.OutWithPrompt("CREATING NEW SAVE", "ENTER YOUR NAME");
            
            try{
                _gameState.NewPlayer(_IO.LastUserInput);
            } catch (InvalidInput)
            {
                _IO.OutWithSubject("ERROR", "This save already exists!");

                Routing(enumResult);
            }
        }

        public void Stats(){
            _IO.OutWithSubject("GAME ENGINE","Loading stats ...");

            _gameState.LoadStats();
        }

        public void LoadGame(){
            _IO.OutWithPrompt("LOADING EXISTING GAME", "ENTER YOUR SAVE NAME");
            _gameState.LoadPlayer(_IO.LastUserInput);
        }

        public void ExitGame(){
            _IO.OutWithSubject("GAME ENGINE", "Attempting shut down ...");
            SystemEvent = new EventModel(EXIT);
        }
        
        public void Routing(OptionMap overrideInput = EVENT_COMPLETE) {
            EventModel nextEvent;
            OptionMap enumResult = overrideInput;

            if(overrideInput == EVENT_COMPLETE){
                _IO.OutWithOptionsPrompt(_workingEvent.EventText, _workingEvent.InputOptions);

                if(!Enum.TryParse<OptionMap>(_IO.LastUserInput, true, out enumResult)){
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