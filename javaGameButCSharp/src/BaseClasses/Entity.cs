using System;
using static JavaGameButCSharp.OptionMap;
using System.Text.Json.Serialization;

namespace JavaGameButCSharp{
    class Entity : StatefulObject{
        [JsonPropertyName("name")]
        public string Name { get; set; } = String.Empty;

        [JsonPropertyName("health")]
        public int Health { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; } = String.Empty;

        [JsonPropertyName(name: "strength")]
        public int Strength { get; set; }

        [JsonPropertyName("speed")]
        public int Speed { get; set; }

        [JsonPropertyName(name: "money")]
        public int Money { get; set; }
        
        [JsonPropertyName("inventory")]
        public Inventory Inventory { get; set; } = new Inventory();

        public Entity(string fileName) : base(fileName, ENTITY){
        }

        public Entity(){
        }
    }
}