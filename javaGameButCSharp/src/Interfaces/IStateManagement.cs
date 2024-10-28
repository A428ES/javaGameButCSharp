using System;

namespace JavaGameButCSharp{
    interface StateManagement{
        public void read(StatefulObject statefulObject);
        public void write(StatefulObject statefulObject);
    }
}