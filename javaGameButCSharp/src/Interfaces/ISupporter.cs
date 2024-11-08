using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    interface ISupporter{
        public void Routing(OptionMap overrideInput = EVENT_COMPLETE);
    }
}