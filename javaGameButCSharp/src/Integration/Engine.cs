using System.ComponentModel.Design;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class Engine{
        private readonly SaveLoadManagement SaveLoad;
        private readonly JsonStateManagement StateManagement;
        private readonly EventController EventController;
        private bool EngineRunning;
        private Entity ActivePlayer;
        private Location ActiveLocation;

        public Engine(){
            EngineRunning = true;
            SaveLoad = new SaveLoadManagement();
            StateManagement = new JsonStateManagement();

            EventModel firstEvent = new(MENUEVENT);
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
        }

        public Location UpdateLocation(string newLocation) => ActiveLocation = new Location(newLocation);

        private void StopEngine() => EngineRunning = false;

        public void EventIntegration(){
            switch(EventController.LastEvent?.EventOutCome){
                case NEW_GAME:
                break;

                case LOAD_GAME:
                break;

                case UPDATE_LOCATION:
                    UpdateLocation(EventController.LastEvent.EventTarget);
                break;

                case EXIT_ENGINE:
                    StopEngine();
                break;
                
                default:
                    EventController.RunNextEvent();
                break;
            }
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
        }
    }
}