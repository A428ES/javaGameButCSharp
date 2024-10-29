using System;

namespace JavaGameButCSharp{
    class StatefulObject{
        string fileName {get;}
        string type {get;}

        public StatefulObject(string fileName, string type){
            this.fileName = fileName;
            this.type = type;
        }

    }
}