namespace JavaGameButCSharp{
    class SupporterContext
    {
        public Event WorkingEvent { get;set; }
        public InputOutManager IO { get; }
        public GameStateController GameState { get; }
        
        public EventModel SystemEvent {get;set;} = new EventModel(OptionMap.MENU_EVENT);


        public SupporterContext(Event workingEvent, InputOutManager io, GameStateController gameState)
        {
            WorkingEvent = workingEvent;
            IO = io;
            GameState = gameState;
        }

        public void ResetToMenu(){
            SystemEvent = new (OptionMap.MENU_EVENT);
        }
    }
}