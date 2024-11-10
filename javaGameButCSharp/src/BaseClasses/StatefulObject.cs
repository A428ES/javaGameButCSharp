namespace JavaGameButCSharp{
    class StatefulObject{
        public string FilePath {get; private set;} = string.Empty;
        private readonly OptionMap _type;

        public StatefulObject(string filePath, OptionMap type){
            FilePath = filePath;
            _type = type;
        }

        public StatefulObject(){
            FilePath = string.Empty;
        }

        public void SetFilePath(string filePath){
            if(FilePath.Equals(string.Empty)){
                FilePath = filePath;
            }
        }

    }
}