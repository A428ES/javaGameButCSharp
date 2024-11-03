namespace JavaGameButCSharp{
    class LocationEvent : Event{
        private readonly Entity _activePlayer;
        private readonly Location _activeLocation;

        public LocationEvent(EventModel eventModel, Entity ActivePlayer) : base(eventModel){
            this._activePlayer = ActivePlayer;
            this._activeLocation = new Location(eventModel.EventTarget);
        }

        public EventModel EventFlow(){
            return new EventModel(OptionMap.EVENT);
        }

        public override void PrintOptions()
        {
            throw new NotImplementedException();
        }

        public override void RecordSelection()
        {
            throw new NotImplementedException();
        }

        public override void PrintOutcome()
        {
            throw new NotImplementedException();
        }
    }
}