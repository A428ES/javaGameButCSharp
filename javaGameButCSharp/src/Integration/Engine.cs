using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class Engine{
        private readonly SaveLoadManagement SaveLoad;
        private readonly StateManagement StateManagement;
        private readonly EventController EventController;
        private bool EngineRunning;
        private Entity ActivePlayer;
        private Location ActiveLocation;

        public Engine(){
            EngineRunning = true;
            SaveLoad = new SaveLoadManagement();
            StateManagement = new JsonStateManagement();

            EventModel firstEvent = new(MENU_EVENT);
            ActivePlayer = new Entity();
            ActiveLocation = new Location();
            EventController = new EventController(StateManagement, SaveLoad,firstEvent);
        }

        public void LoadPlayer(string playerName){
            SaveLoad.LoadUser(playerName);

            ActivePlayer = StateManagement.Read<Entity>(SaveLoad.GetStatePath(ENTITY, "PLAYER"));
            UpdateLocation(ActivePlayer.Location);

            EventController.ActivePlayer = ActivePlayer;
            EventController.ActiveLocation = ActiveLocation;
        }

        public void NewPlayer(string newPlayer){
            SaveLoad.NewSave(newPlayer);

            LoadPlayer(newPlayer);
            
            ActivePlayer.Name = newPlayer;
            StateManagement.Write(SaveLoad.GetStatePath(ENTITY, "PLAYER"), ActivePlayer);
        }

        public void LoadStats(){
            Console.WriteLine(EventController.ActivePlayer.Name);
            Console.WriteLine(EventController.ActivePlayer.Health);
        }

        public void UpdateLocation(string newLocation) {
            string locationPath = SaveLoad.GetStatePath(LOCATION, newLocation);
            
            ActiveLocation =  StateManagement.Read<Location>(locationPath);
            EventController.ActiveLocation = ActiveLocation;
        }

        private void StopEngine() => EngineRunning = false;

        public void EventIntegration(){
            switch(EventController.CurrentEvent.EventType){
                case NEW:
                    NewPlayer(EventController.CurrentEvent.EventTarget);
                break;

                case LOAD:
                    LoadPlayer(EventController.CurrentEvent.EventTarget);
                break;

                case STATS:
                    LoadStats();
                break;
            
                case UPDATE_LOCATION:
                    UpdateLocation(EventController.CurrentEvent.EventTarget);
                break;

                case EXIT:
                    StopEngine();
                break;
                
                default:
                    EventController.RunNextEvent();

                    return;
            }

            EventController.ReturnToMenu();
        }

        public void EngineLoop(){
            while(EngineRunning){
                try{
                    EventIntegration();
                } catch(InvalidInput e){
                    Console.WriteLine("Error: Invalid game input provided. Additional details: ");
                    Console.WriteLine(e.Message);
                } catch (ResourceNotFound e){
                    Console.WriteLine("Error: Expected game resource not found. Additional details: ");
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Thank you for playing! GOOD BYE!");
        }
    }
}