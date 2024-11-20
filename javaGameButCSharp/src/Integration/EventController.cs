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
        public EventController(StateManagement stateManagement, SaveLoadManagement saveLoad, GameStateController gameState, DisplayContext displayContext){
            CurrentEvent = new(MENU_EVENT);
            LastEvent = EventModel.Copy(CurrentEvent);
            _displayContext = displayContext;
            displayContext.Renderer.GameMenu.converter = GUIMenu_Injection;

            _IO = new InputOutManager();
            _gameState = gameState;

            SupporterContext supporterContext = new(new Event(), _IO, _gameState);

            _eventSupporter = new EventSupporter(stateManagement, saveLoad, supporterContext);
        }
        
        public void GUIMenu_Injection(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem) {
                if (menuItem.Tag is OptionMap option) {
                    RouteMenuEvent(option.ToString());
                } else {
                    _IO.CentralErrorOutput("Invalid option type in MenuItem tag.");
                }
            }
        }

        public void RouteMenuEvent(string inputInject){
            if (string.IsNullOrEmpty(inputInject)) {
                _IO.CentralErrorOutput("Invalid option type in MenuItem tag.");
                return;
            }
            
            CurrentEvent = new EventModel(MENU_EVENT);
            _IO.LastUserInput = inputInject;
                    
            RunNextEvent();
        }

        public void RunNextEvent(){
            Supporter supporterObject = _eventSupporter.RunEvent(CurrentEvent);
            _displayContext.UpdateMenu(supporterObject.MapRoute());

            LastEvent.EventOutCome = _eventSupporter.EventOutCome;
            CurrentEvent = _eventSupporter.NextEvent;

            if(!_gameState.ActivePlayer.Name.Equals("") && !_displayContext.PlayerLoaded()){
                string statusMsg = $"[Name: {_gameState.ActivePlayer.Name}] [Health: {_gameState.ActivePlayer.Health}]";

                _displayContext.SpawnPlayer(); 
                _displayContext.Renderer.ActivityController.UnPauseGame();

                _displayContext.Renderer.StatusBar.UpdatePlayerStats(statusMsg);
            } else if(_gameState.ActivePlayer.Name.Equals("") && _displayContext.PlayerLoaded()) {
                _displayContext.DestroyPlayer();
                _displayContext.Renderer.ActivityController.PauseGame();
            } else if(_gameState.ActivePlayer.Name.Equals("") && !_displayContext.PlayerLoaded()) {
                _displayContext.Renderer.ActivityController.PauseGame();
                _displayContext.Renderer.StatusBar.UpdatePlayerStats("No player logged in.");
            }
        }

        public void ReturnToMenu(){
            CurrentEvent = new EventModel(MENU_EVENT);
        }
    }
}