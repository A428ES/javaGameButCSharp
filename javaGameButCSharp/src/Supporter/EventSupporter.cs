using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventSupporter{
        private readonly StateManagement _stateManagement;
        private readonly SaveLoadManagement _saveLoad;
        private readonly InputOutManager _IO;
        private Event _workingEvent;
        public EventModel NextEvent {get;set;}
        public OptionMap EventOutCome {get;set;}


        public EventSupporter(StateManagement stateManagement, SaveLoadManagement saveLoad, InputOutManager IO){
            _stateManagement = stateManagement;
            _saveLoad = saveLoad;
            _workingEvent = new MenuEvent();
            _IO = IO;

            NextEvent = new EventModel(EVENT);
            EventOutCome = EVENT_IN_PROGRESS;
        }

        public Event LoadEvent(EventModel nextEvent){
            string eventPath = _saveLoad.GetStatePath(EVENT, nextEvent.EventType.ToString());
            
            return _stateManagement.Read<Event>(eventPath);
        }

        public void Menu(){
            MainMenuEventSupporter menuSupport = new(_workingEvent, _IO);

            NextEvent = menuSupport.Routing();
        }

        public void Battle(){

        }

        public void Location(){

        }

        public void RunLastEvent(){

        }

        public void RunEvent(EventModel nextEvent){
            _workingEvent = LoadEvent(nextEvent);

            switch(nextEvent.EventType){
                case MENU_EVENT:
                    Menu();
                    break;
                case BATTLE_EVENT:
                    Battle();
                    break;
                case LOCATION_EVENT:
                    Location();
                    break;
                default:
                    RunLastEvent();
                    break;

            }
        }
    }
}