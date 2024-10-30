using System;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class StatefulObject{
        string FileName {get;}
        OptionMap Type {get;}

        public StatefulObject(string fileName, OptionMap type){
            this.FileName = fileName;
            this.Type = type;
        }

        public StatefulObject(){
            this.FileName = string.Empty;
        }

    }
}