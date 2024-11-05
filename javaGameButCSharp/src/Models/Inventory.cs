using System;
using System.Text.Json.Serialization;

namespace JavaGameButCSharp{
    class Inventory{
        [JsonPropertyName("activeWeapon")]
        public string ActiveWeapon { get; set; } = string.Empty;

        [JsonPropertyName("activeArmor")]
        public string ActiveArmor { get; set; } = string.Empty;

        [JsonPropertyName("inventory")]
        public List<Item> EntityInventory {get; set;} = [];

        public Inventory(){
        }
    }


}