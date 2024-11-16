using System.Windows;
using System.Windows.Controls;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class EventController{
        public EventModel CurrentEvent {get;set;}
        public EventModel LastEvent {get;set;}
        private readonly EventSupporter _eventSupporter;
        private readonly InputOutManager _IO;
        private readonly GameStateController _gameState;
        private readonly DisplayContext _displayContext;
        private Sprite sprite {get;set;}
    
        public EventController(StateManagement stateManagement, SaveLoadManagement saveLoad, GameStateController gameState, DisplayContext displayContext){
            CurrentEvent = new(MENU_EVENT);
            LastEvent = EventModel.Copy(CurrentEvent);
            _displayContext = displayContext;
            displayContext.GameMenu.converter = GUIMenu_Injection;

            _IO = new InputOutManager();
            _gameState = gameState;

            SupporterContext supporterContext = new(new Event(), _IO, _gameState);

            _eventSupporter = new EventSupporter(stateManagement, saveLoad, supporterContext);
        }

        public void GUIMenu_Injection(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                if (menuItem.Tag is OptionMap option)
                {
                    CurrentEvent = new EventModel(MENU_EVENT);
                    _IO.LastUserInput = option.ToString();
                    
                    RunNextEvent();
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid option type in MenuItem tag.");
                }
            }
        }

        public void ClearCanvas(){}
        public void RunNextEvent(){
            Supporter supporterObject = _eventSupporter.RunEvent(CurrentEvent);

            LastEvent.EventOutCome = _eventSupporter.EventOutCome;
            CurrentEvent = _eventSupporter.NextEvent;

            _displayContext.GameMenu.LoadMenuStack("MENU", supporterObject.MapRoute());

            if(!_gameState.ActivePlayer.Name.Equals("") && sprite == null){
                sprite = new Sprite(
                    @"C:\Walk.png", 
                    new System.Windows.Point(100, 100), 
                    _displayContext.GameCanvas, 
                    _displayContext.Boundary,
                    frameCount: 6, 
                    frameWidth: 42, 
                    frameHeight: 42
                );
            } else if(_gameState.ActivePlayer.Name.Equals("") && sprite != null){
                sprite.DestroySprite();
                sprite = null;
            }
        }

        public void ReturnToMenu(){
            CurrentEvent = new EventModel(MENU_EVENT);
        }
    }
}