using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class Engine{
        private readonly EventController _eventController;
        private readonly GameStateController _gameState;
        private bool _engineRunning;

        public Engine(string bootPath){
            _engineRunning = true;

            StateManagement stateManagement = new JsonStateManagement();
            SaveLoadManagement saveLoad = stateManagement.Read<SaveLoadManagement>(bootPath);
            saveLoad.LoadConfig();
            
            _gameState = new GameStateController(saveLoad, stateManagement);
            _eventController = new EventController(stateManagement, saveLoad, _gameState);
        }

        private void StopEngine() => _engineRunning = false;

        public void EventIntegration(){
            switch(_eventController.CurrentEvent.EventType){
                case EXIT:
                    StopEngine();
                break;
                
                default:
                    _eventController.RunNextEvent();

                    return;
            }

            _eventController.ReturnToMenu();
        }

        public void EngineLoop(){
            while(_engineRunning){
                try{
                    EventIntegration();
                } catch(InvalidInput e){
                    Console.WriteLine("Error: Invalid game input provided. Additional details: ");
                    Console.WriteLine(e.Message);

                    _eventController.ReturnToMenu();
                } catch (ResourceNotFound e){
                    Console.WriteLine("Error: Expected game resource not found. Additional details: ");
                    Console.WriteLine(e.Message);

                    break;
                }
            }

            Console.WriteLine("Thank you for playing! GOOD BYE!");
        }
    }
}