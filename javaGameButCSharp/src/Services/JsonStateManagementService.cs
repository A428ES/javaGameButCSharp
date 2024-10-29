using System;
using System.Text.Json;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class JsonStateManagement : StateManagement{
        SaveLoadManagement saveLoadManagement {get;}

        public JsonStateManagement(SaveLoadManagement saveLoadManagement){
            this.saveLoadManagement = saveLoadManagement;
        }
        
        public StatefulObject read(string filePath, OptionMap type){
            if(!File.Exists(filePath)){
                throw new ResourceNotFound(filePath);
            }

            string jsonData = File.ReadAllText(filePath);

            return type switch {
                ENTITY => JsonSerializer.Deserialize<Entity>(jsonData),
                LOCATION => JsonSerializer.Deserialize<Location>(jsonData),
                EVENT => JsonSerializer.Deserialize<Event>(jsonData),
                _ => throw new JsonException($"Unknown type: {type}")
            };
        }

        public void write(StatefulObject statefulObject){

        }
    }




}