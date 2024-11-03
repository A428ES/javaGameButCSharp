using System.IO.Compression;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventSupporter{
        private readonly JsonStateManagement _stateManagement;
        private readonly SaveLoadManagement _saveLoad;
        private readonly Event _workingEvent;
        public EventModel NextEvent {get;}
        public OptionMap EventOutCome {get;}


        public EventSupporter(JsonStateManagement stateManagement, SaveLoadManagement saveLoad){
            _stateManagement = stateManagement;
            _saveLoad = saveLoad;
            _workingEvent = new MenuEvent();

            NextEvent = new EventModel(EVENT);
            EventOutCome = IN_PROGRESS;
        }

        public Event LoadEvent(EventModel nextEvent){
            string eventPath = _saveLoad.GetStatePath(EVENT, nextEvent.EventType.ToString());
            
            return _stateManagement.Read<Event>(eventPath);
        }


        public void Menu(){

        }

        public void Battle(){

        }

        public void Location(){

        }

        public void RunLastEvent(){

        }

        public EventModel RunEvent(EventModel nextEvent){
            Event workingEvent = LoadEvent(nextEvent);

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

            return new EventModel(MENU, REPEAT, "TEST");
        }
    }
}