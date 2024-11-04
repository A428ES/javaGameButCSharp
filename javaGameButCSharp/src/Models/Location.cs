using System.Text.Json.Serialization;
using static JavaGameButCSharp.OptionMap;


namespace JavaGameButCSharp{
    class Location : StatefulObject{
        [JsonPropertyName("name")]
        public string Name {get; set;}

        [JsonPropertyName("description")]
        public string Description {get; set;}

        [JsonPropertyName("nextLocation")]
        public string NextLocation {get; set;}

        [JsonPropertyName("previousLocation")]
        public string PreviousLocation {get; set;}

        [JsonPropertyName("npcList")]   
        public List<String> NpcLis {get; set;}     
        
        
        public Location(string fileName) : base(fileName, LOCATION){
        }

        public Location(){
            
        }
    }
}