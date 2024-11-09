using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventSupporter{
        private readonly StateManagement _stateManagement;
        private readonly SaveLoadManagement _saveLoad;
        public SupporterContext _supporterContext;
        public EventModel NextEvent {get;set;}
        public OptionMap EventOutCome {get;set;}


        public EventSupporter(StateManagement stateManagement, SaveLoadManagement saveLoad, SupporterContext supporterContext){
            _stateManagement = stateManagement;
            _saveLoad = saveLoad;
            _supporterContext = supporterContext;

            NextEvent = new EventModel(EVENT);
            EventOutCome = EVENT_IN_PROGRESS;
        }

        public Event LoadEvent(EventModel nextEvent){
            string eventPath = _saveLoad.GetStatePath(EVENT, nextEvent.EventType.ToString());
            
            return _stateManagement.Read<Event>(eventPath);
        }

        public void RunEvent(EventModel nextEvent){
            _supporterContext.WorkingEvent = LoadEvent(nextEvent);

            Supporter eventSupport = SupporterFactory.GenerateSupporter(nextEvent.EventType, _supporterContext);
            
            eventSupport.Routing(eventSupport.MapRoute());
            NextEvent = _supporterContext.SystemEvent;
        }
    }
}