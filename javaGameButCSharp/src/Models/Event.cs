using System;
using static JavaGameButCSharp.OptionMap;


namespace JavaGameButCSharp{
    class Event : StatefulObject{

        public EventModel eventModel {get;}

        public OptionMap result {get;}

        public Event(string fileName): base(fileName, EVENT){
        }

        public Event(){
            
        }
    }
}