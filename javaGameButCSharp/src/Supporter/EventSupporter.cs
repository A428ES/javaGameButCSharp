namespace JavaGameButCSharp{
    class EventSupporter(){
        public EventModel NextEvent {get;}
        public OptionMap EventOutCome {get;}
        public EventModel RunEvent(Event nextEvent){
            return new EventModel(OptionMap.REPEAT, OptionMap.REPEAT, "TEST");
        }
    }
}