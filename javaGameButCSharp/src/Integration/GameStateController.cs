using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class GameStateController{
        private readonly SaveLoadManagement _saveLoad;
        private readonly StateManagement _stateManagement;
        public Entity ActivePlayer {get;set;}
        public Entity ActiveTargetNPC {get;set;}
        public Location ActiveLocation {get;set;}
        public Item ActiveItem {get;set;}
        public bool InBattle{get;set;} = false;

        public GameStateController(SaveLoadManagement saveLoad, StateManagement stateManagement){
            _saveLoad = saveLoad;
            _stateManagement = stateManagement;

            ActiveLocation = new Location();
            ActivePlayer = new Entity();
            ActiveTargetNPC = new Entity();
            ActiveItem = new Item();
        }

        public void LoadPlayer(string playerName) {       
            _saveLoad.LoadUser(playerName);

            ActivePlayer = _stateManagement.Read<Entity>(_saveLoad.GetStatePath(ENTITY, "PLAYER"));
            UpdateLocation(ActivePlayer.Location);
        }

        public void LoadNPCTarget(string npcName){
            ActiveTargetNPC = _stateManagement.Read<Entity>(_saveLoad.GetStatePath(ENTITY, npcName));
        }

        public void LoadItem(Item item){
            ActiveItem = item;
        }

        public void SavePlayer(){
            _stateManagement.Write(ActiveLocation.FilePath, ActivePlayer);
        }

        public void SaveNPCTarget(){
            _stateManagement.Write(ActiveTargetNPC.FilePath, ActiveTargetNPC);
        }

        public void SaveLocation(){
            _stateManagement.Write(ActiveLocation.FilePath, ActiveLocation);
        }

        public void SaveAllStates(){
            SavePlayer();
            SaveNPCTarget();
            SaveLocation();
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

        public void ResetUser(){
            ActivePlayer = new Entity();
            ActiveLocation = new Location();
        }

        public void DeleteSave(string saveToDelete){
            _saveLoad.DeleteSave(saveToDelete);

            if(saveToDelete.Equals(ActivePlayer.Name)){
                ResetUser();
            }
        }

        public List<string?> GetGameSaves(){
            return _saveLoad.GetSaveGameList();
        }
    }
}