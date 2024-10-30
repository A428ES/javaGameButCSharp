using System;
using System.Text.Json;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class JsonStateManagement : StateManagement{

        public JsonStateManagement(){
        }
        
        public T Read<T>(string filePath) where T : StatefulObject
        {
            if (!File.Exists(filePath))
            {
                throw new ResourceNotFound(filePath);
            }

            string jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonData) 
                ?? throw new JsonException($"Failed to deserialize {typeof(T).Name}");
        }
        public void Write(StatefulObject statefulObject){

        }
    }

}