namespace JavaGameButCSharp{
    class EventSupporter(){
        public EventModel RunEvent(Event nextEvent){
            return new EventModel(OptionMap.REPEAT, OptionMap.REPEAT, "TEST");
        }
    }
}