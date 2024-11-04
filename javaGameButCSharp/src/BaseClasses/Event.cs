using System;
using System.Text.Json.Serialization;
using static JavaGameButCSharp.OptionMap;


namespace JavaGameButCSharp{
    class Event : StatefulObject{
        [JsonPropertyName("name")] 
        public string Name {get;set;} = String.Empty;

        [JsonPropertyName("eventText")]
        public string EventText {get;set;} = String.Empty;

        [JsonPropertyName("inputOptions")]
        public List<String> InputOptions { get; set; } = [];

        public EventModel Model {get;set;}

        public OptionMap Result {get;}

        public Event(EventModel eventModel): base(eventModel.EventType.ToString(), EVENT){
            Model = eventModel;
            Result = eventModel.EventOutCome;
        }

        public Event(){

        }

        public static void LoadTarget(Entity target) => throw new Exception("Not implemented");
        public static void LoadTarget(Location target) => throw new Exception("Not implemented");
    }
}