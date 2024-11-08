namespace JavaGameButCSharp{
    interface StateManagement{
        public T Read<T>(string filePath) where T : StatefulObject;
        public void Write<T>(string filePath, T statefulObject) where T : StatefulObject;
    }
}