namespace JavaGameButCSharp{
    class MenuEvent : Event, IEventFeed
    {
        public MenuEvent(EventModel eventModel) : base(eventModel)
        {
        }

        public MenuEvent(){

        }

        public void PrintOptions()
        {
            throw new NotImplementedException();
        }

        public void PrintOutcome()
        {
            throw new NotImplementedException();
        }

        public void RecordSelection()
        {
            throw new NotImplementedException();
        }
    }
}