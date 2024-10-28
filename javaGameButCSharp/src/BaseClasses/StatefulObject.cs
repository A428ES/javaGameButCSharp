using System;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class StatefulObject{
        public StatefulObject(OptionMap objectType){
            switch(objectType){
                case EVENT:
                    Console.WriteLine("event");
                break;

                case LOCATION:
                    Console.WriteLine("location");
                break;

                case ENTITY:
                    Console.WriteLine("entity");
                break;

                default:
                    Console.WriteLine("default");
                break;
            }
        }
    }
}