namespace JavaGameButCSharp{
    class StatefulObject{
        private readonly string _fileName;
        private readonly OptionMap _type;

        public StatefulObject(string fileName, OptionMap type){
            _fileName = fileName;
            _type = type;
        }

        public StatefulObject(){
            _fileName = string.Empty;
        }

    }
}