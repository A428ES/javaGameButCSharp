namespace JavaGameButCSharp{
    class SupporterContext
    {
        public Event WorkingEvent { get;set; }
        public InputOutManager IO { get; }
        public GameStateController GameState { get; }
        public RetryHelper RetryHelper {get;}
        
        public EventModel SystemEvent {get;set;} = new EventModel(OptionMap.MENU_EVENT);


        public SupporterContext(Event workingEvent, InputOutManager io, GameStateController gameState)
        {
            WorkingEvent = workingEvent;
            IO = io;
            GameState = gameState;
            RetryHelper = new RetryHelper(IO);
        }

        public void ResetToMenu(){
            SystemEvent = new (OptionMap.MENU_EVENT);
        }

        public void ResumeGame(){
            SystemEvent = new(OptionMap.LOCATION_EVENT, GameState.ActiveLocation.Name);
        }
    }
}