using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventController{
        public EventModel CurrentEvent {get;set;}
        public EventModel LastEvent {get;set;}
        private readonly JsonStateManagement jsonStateManagement;
        private readonly SaveLoadManagement saveLoadManagement;
        private readonly EventSupporter _eventSupporter;
        public Entity ActivePlayer;
        public Location ActiveLocation;
    
        public EventController(JsonStateManagement stateManagement, SaveLoadManagement saveLoad, EventModel currentEvent){
            this.jsonStateManagement = stateManagement;
            this.saveLoadManagement = saveLoad;
            this.CurrentEvent = currentEvent;
            this.LastEvent = EventModel.Copy(currentEvent);
            this._eventSupporter = new EventSupporter(stateManagement, saveLoad);
            this.ActiveLocation = new Location();
            this.ActivePlayer = new Entity();
        }

        public void RunNextEvent(){
            _eventSupporter.RunEvent(CurrentEvent);

            LastEvent.EventOutCome = _eventSupporter.EventOutCome;
            CurrentEvent = _eventSupporter.NextEvent;
        }
    }
}