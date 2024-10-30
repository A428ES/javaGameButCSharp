using System;
using System.Text.Json.Serialization;

namespace JavaGameButCSharp{
    class Inventory{
        [JsonPropertyName("activeWeapon")]
        public string ActiveWeapon { get; set; }

        [JsonPropertyName("activeArmor")]
        public string ActiveArmor { get; set; }

        [JsonPropertyName("inventory")]
        public List<Item> EntityInventory {get; set;}

        public Inventory(){
            this.ActiveArmor = String.Empty;
            this.ActiveWeapon = String.Empty;
            this.EntityInventory = [];
        }
    }


}