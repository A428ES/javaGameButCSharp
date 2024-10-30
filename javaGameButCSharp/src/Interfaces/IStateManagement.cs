using System;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    interface StateManagement{
        public T Read<T>(string filePath) where T : StatefulObject;
        public void Write(StatefulObject statefulObject);
    }
}