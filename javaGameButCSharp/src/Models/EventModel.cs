using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventModel
    {
        public OptionMap EventType { get; }
        public string EventTarget { get; }
        public OptionMap EventOutCome {get;set;}
        
        public static EventModel Copy(EventModel toCopy){
            return new EventModel(toCopy.EventType, toCopy.EventOutCome, toCopy.EventTarget);
        }

        public EventModel(OptionMap type){
            this.EventType = type;
            this.EventOutCome = EVENT_IN_PROGRESS;
            this.EventTarget = string.Empty;
        }

        public EventModel(OptionMap type, string target){
            this.EventType = type;
            this.EventTarget = target;
            this.EventOutCome = EVENT_IN_PROGRESS;
        }

        public EventModel(OptionMap type, OptionMap outcome, string target){
            this.EventType = type;
            this.EventTarget = target;
            this.EventOutCome = outcome;
        }
    }
}
