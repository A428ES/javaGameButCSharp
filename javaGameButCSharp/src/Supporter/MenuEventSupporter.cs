using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class MainMenuEventSupporter{
        private InputOutManager _IO;
        private Event _workingEvent;

        public MainMenuEventSupporter(Event workingEvent, InputOutManager io){
            _IO = io;
            _workingEvent = workingEvent;
        }
        
        public EventModel Routing() {
            EventModel nextEvent;

            _IO.OutWithOptionsPrompt(_workingEvent.EventText, _workingEvent.InputOptions);

            if(!Enum.TryParse<OptionMap>(_IO.LastUserInput, true, out OptionMap enumResult)){
                throw new InvalidInput("Not a valid game engine input");
            }
            
            switch(enumResult){
                case EXIT:
                    _IO.OutWithSubject("GAME ENGINE", "Attempting shut down ...");
                    nextEvent = new EventModel(EXIT);
                break;
                case NEW:
                    _IO.OutWithPrompt("CREATING NEW SAVE", "ENTER YOUR NAME");
                    nextEvent = new EventModel(NEW, _IO.LastUserInput);
                break;
                case LOAD:
                    _IO.OutWithPrompt("LOADING EXISTING GAME", "ENTER YOUR SAVE NAME");
                    nextEvent = new EventModel(LOAD, _IO.LastUserInput);
                break;
                case STATS:
                    _IO.OutWithSubject("GAME ENGINE","Loading stats ...");
                    nextEvent = new EventModel(STATS, _IO.LastUserInput);
                break;
                default:
                    nextEvent = new EventModel(LOCATION_EVENT, "tatooine");
                break;
            }

            return nextEvent;
        }
    }
}