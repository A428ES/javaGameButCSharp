using System;
using System.Text.Json.Serialization;

namespace JavaGameButCSharp{
    class Item{
        [JsonPropertyName("modifier")]
        public int Modifier {get; set;}

        [JsonPropertyName("condition")]
        public int Condition {get; set;}

        [JsonPropertyName("value")]
        public int Value {get; set;}

        [JsonPropertyName("type")]
        public string Type {get; set;}

        public Item(){
            this.Type = String.Empty;
        }
    }
}