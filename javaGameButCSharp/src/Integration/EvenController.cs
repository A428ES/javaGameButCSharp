using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventController{
        public EventModel CurrentEvent {get;set;}
        public EventModel LastEvent {get;set;}
        private readonly EventSupporter _eventSupporter;
        private readonly InputOutManager _IO;
        private readonly GameStateController _gameState;
    
        public EventController(StateManagement stateManagement, SaveLoadManagement saveLoad, GameStateController gameState){
            CurrentEvent = new(MENU_EVENT);
            LastEvent = EventModel.Copy(CurrentEvent);

            _IO = new InputOutManager();
            _gameState = gameState;

            SupporterContext supporterContext = new(new Event(), _IO, _gameState);

            _eventSupporter = new EventSupporter(stateManagement, saveLoad, supporterContext);
        }

        public void RunNextEvent(){
            _eventSupporter.RunEvent(CurrentEvent);

            LastEvent.EventOutCome = _eventSupporter.EventOutCome;
            CurrentEvent = _eventSupporter.NextEvent;
        }

        public void ReturnToMenu(){
            CurrentEvent = new EventModel(MENU_EVENT);
            QuickResolveEvent();
        }

        public void QuickResolveEvent(){
            LastEvent.EventOutCome = EVENT_COMPLETE;
        }
    }
}