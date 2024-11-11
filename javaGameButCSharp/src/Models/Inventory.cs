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

        public Item GetItem(string itemID){
            return EntityInventory.FirstOrDefault(item => item.Name.Equals(itemID.ToUpper()) && item.Quantity > 0) ?? throw new ResourceNotFound("Cannot locate item");
        }

        public Item GetActiveArmor(){
            return GetItem(ActiveArmor);
        }

        public Item GetActiveWeapon(){
            return GetItem(ActiveWeapon);
        }

        public List<Item> GetItemType(string type){
            return EntityInventory.Where(item => item.Type.Equals(type.ToUpper()) && item.Quantity > 0).ToList();
        }
    }
}