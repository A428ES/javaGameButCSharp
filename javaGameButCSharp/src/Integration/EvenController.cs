using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventController{
        public EventModel CurrentEvent {get;set;}
        public EventModel LastEvent {get;set;}
        private readonly EventSupporter EventSupporter;
        private readonly JsonStateManagement jsonStateManagement;
        private readonly SaveLoadManagement saveLoadManagement;
        public Entity ActivePlayer;
        public Location ActiveLocation;
    
        public EventController(JsonStateManagement stateManagement, SaveLoadManagement saveLoad, EventModel currentEvent){
            this.jsonStateManagement = stateManagement;
            this.saveLoadManagement = saveLoad;
            this.CurrentEvent = currentEvent;
            this.LastEvent = EventModel.Copy(currentEvent);
            this.ActiveLocation = new Location();
            this.ActivePlayer = new Entity();

            EventSupporter = new EventSupporter();
        }

        public void RunNextEvent(){
            Event eventRun = new(CurrentEvent.EventType.ToString());
        }
    }
}