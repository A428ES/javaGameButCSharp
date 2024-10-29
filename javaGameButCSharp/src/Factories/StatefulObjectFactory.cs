using System;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    abstract class StatefulObjectFactory{
        private StateManagement stateManagement;

        public StatefulObjectFactory(StateManagement stateManagement){
            this.stateManagement = stateManagement;
        }

        private void generate(OptionMap generateType){
        }

        private void filePathBuilder(string fileName, string fileType){

        }
    }
}