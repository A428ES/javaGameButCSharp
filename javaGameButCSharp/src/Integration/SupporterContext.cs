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
            if(GameState.InBattle){
                SystemEvent = new (OptionMap.BATTLE_EVENT, GameState.ActiveTargetNPC.Name);
            } else {
                SystemEvent = new(OptionMap.LOCATION_EVENT, GameState.ActiveLocation.Name);
            }
        }

        public void ToggleBattleOn(){
            GameState.InBattle = true;
        }

        public void ToggleBattleOff(){
            GameState.InBattle = false;
        }

        public void Inventory(){
            SystemEvent = new(OptionMap.INVENTORY_EVENT, "");
        } 

        public bool LoggedIn(){
            return !string.IsNullOrEmpty(GameState.ActivePlayer.Name);
        }
    }
}