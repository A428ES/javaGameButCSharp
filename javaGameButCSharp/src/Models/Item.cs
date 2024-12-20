using System;
using System.Text.Json.Serialization;

namespace JavaGameButCSharp{
    class Item{
        [JsonPropertyName("name")]
        public string Name {get;set;}
        
        [JsonPropertyName("description")]
        public string Description {get;set;}

        [JsonPropertyName("modifier")]
        public int Modifier {get; set;}

        [JsonPropertyName("condition")]
        public int Condition {get; set;}

        [JsonPropertyName("value")]
        public int Value {get; set;}

        [JsonPropertyName("type")]
        public string Type {get; set;}
        [JsonPropertyName("quantity")]
        public int Quantity {get; set;}

        public Item(){
            this.Type = String.Empty;
        }
    }
}