using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventController{
        public EventModel CurrentEvent {get;set;}
        public EventModel LastEvent {get;set;}
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
        }

        public void RunNextEvent(){
            EventSupporter processEvent = new EventSupporter();
            Event eventToRun = new(CurrentEvent.EventType.ToString());

            processEvent.RunEvent(eventToRun);

            LastEvent.EventOutCome = processEvent.EventOutCome;
            CurrentEvent = processEvent.NextEvent;
        }
    }
}