using System;

namespace JavaGameButCSharp{
    class JsonStateManagement : StateManagement{
        SaveLoadManagement saveLoadManagement {get;}

        public JsonStateManagement(SaveLoadManagement saveLoadManagement){
            this.saveLoadManagement = saveLoadManagement;
        }
        
        public void read(StatefulObject statefulObject){

        }

        public void write(StatefulObject statefulObject){

        }
    }




}