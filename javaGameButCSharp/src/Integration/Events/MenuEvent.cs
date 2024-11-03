namespace JavaGameButCSharp{
    class MenuEvent : Event
    {
        public MenuEvent(EventModel eventModel) : base(eventModel)
        {
        }

        public MenuEvent() : base(new EventModel(OptionMap.ENTITY)){

        }

        public override void PrintOptions()
        {
            throw new NotImplementedException();
        }

        public override void PrintOutcome()
        {
            throw new NotImplementedException();
        }

        public override void RecordSelection()
        {
            throw new NotImplementedException();
        }
    }
}