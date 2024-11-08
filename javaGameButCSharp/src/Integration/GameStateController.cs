using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class GameStateController{
        private readonly SaveLoadManagement _saveLoad;
        private readonly StateManagement _stateManagement;
        public Entity ActivePlayer {get;set;}
        public Entity ActiveTargetNPC {get;set;}
        public Location ActiveLocation {get;set;}

        public GameStateController(SaveLoadManagement saveLoad, StateManagement stateManagement){
            _saveLoad = saveLoad;
            _stateManagement = stateManagement;

            ActiveLocation = new Location();
            ActivePlayer = new Entity();
            ActiveTargetNPC = new Entity();
        }

        public void LoadPlayer(string playerName) {       
            _saveLoad.LoadUser(playerName);

            ActivePlayer = _stateManagement.Read<Entity>(_saveLoad.GetStatePath(ENTITY, "PLAYER"));
            UpdateLocation(ActivePlayer.Location);
        }

        public void LoadNPCTarget(string npcName){
            ActiveTargetNPC = _stateManagement.Read<Entity>(_saveLoad.GetStatePath(ENTITY, npcName));
        }

        public void NewPlayer(string newPlayer){
            _saveLoad.NewSave(newPlayer);

            LoadPlayer(newPlayer);
            
            ActivePlayer.Name = newPlayer;
            _stateManagement.Write(_saveLoad.GetStatePath(ENTITY, "PLAYER"), ActivePlayer);
        }

        public void LoadStats(){
            if(ActivePlayer.Name.Equals(string.Empty)){
                Console.WriteLine("No user loaded!");

                return;
            }

            Console.WriteLine(ActivePlayer.Name);
            Console.WriteLine(ActivePlayer.Health);
        }

        public void UpdateLocation(string newLocation) {
            string locationPath = _saveLoad.GetStatePath(LOCATION, newLocation);

            ActivePlayer.Location = newLocation;
            ActiveLocation =  _stateManagement.Read<Location>(locationPath);
        }
    }
}