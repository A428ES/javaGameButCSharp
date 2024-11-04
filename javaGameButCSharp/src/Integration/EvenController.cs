using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventController{
        public EventModel CurrentEvent {get;set;}
        public EventModel LastEvent {get;set;}
        private readonly EventSupporter _eventSupporter;
        private readonly InputOutManager _IO;
        public Entity ActivePlayer;
        public Location ActiveLocation;
    
        public EventController(StateManagement stateManagement, SaveLoadManagement saveLoad, EventModel currentEvent){
            this.CurrentEvent = currentEvent;
            this.LastEvent = EventModel.Copy(currentEvent);
            this.ActiveLocation = new Location();
            this.ActivePlayer = new Entity();
            this._IO = new InputOutManager();
            this._eventSupporter = new EventSupporter(stateManagement, saveLoad, _IO);
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