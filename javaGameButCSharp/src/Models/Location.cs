using System.Text.Json.Serialization;
using static JavaGameButCSharp.OptionMap;


namespace JavaGameButCSharp{
    class Location : StatefulObject{
        [JsonPropertyName("name")]
        public string Name {get; set;} = string.Empty;

        [JsonPropertyName("description")]
        public string Description {get; set;} = string.Empty;

        [JsonPropertyName("nextLocation")]
        public string NextLocation {get; set;} = string.Empty;

        [JsonPropertyName("previousLocation")]
        public string PreviousLocation {get; set;} = string.Empty;

        [JsonPropertyName("npcList")]   
        public List<String> NpcLis {get; set;} = []; 
        
        
        public Location(string fileName) : base(fileName, LOCATION){
            this.Name = string.Empty;
            this.NextLocation = string.Empty;
        }

        public Location(){
            
        }
    }
}