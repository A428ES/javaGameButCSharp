using System.Windows;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class Engine{
        private readonly EventController _eventController;
        private readonly GameStateController _gameState;
        DisplayContext _displayContext;

        public Engine(string bootPath, DisplayContext displayContext){
            _displayContext = displayContext;

            StateManagement stateManagement = new JsonStateManagement();
            SaveLoadManagement saveLoad = stateManagement.Read<SaveLoadManagement>(bootPath);
            saveLoad.LoadConfig();
            
            _gameState = new GameStateController(saveLoad, stateManagement);
            _eventController = new EventController(stateManagement, saveLoad, _gameState, _displayContext);
        }
        public void EngineLoop(){
            _eventController.RunNextEvent();
        }
    }
}