using System;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    interface StateManagement{
        public StatefulObject read(string filePath, OptionMap type);
        public void write(StatefulObject statefulObject);
    }
}