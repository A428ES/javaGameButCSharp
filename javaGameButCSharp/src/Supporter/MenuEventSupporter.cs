using System.Windows;
using System.Windows.Media.Animation;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class MainMenuEventSupporter(SupporterContext supporterContext) : Supporter(supporterContext){

        public void NewGame(){
            _supporterContext.IO.OutWithPrompt("CREATING NEW SAVE", "ENTER YOUR NAME");
            
            _supporterContext.GameState.NewPlayer(_supporterContext.IO.LastUserInput);

            _supporterContext.IO.NotifyUser("New user created");
            LoadPlayerLocation();
        }

        public void LoadPlayerLocation(){
            _supporterContext.SystemEvent = new EventModel(LOCATION_EVENT, _supporterContext.GameState.ActivePlayer.Location);
        }

        public void LoadGame(){
            _supporterContext.IO.OutWithPrompt("LOADING EXISTING GAME", "ENTER YOUR SAVE NAME");
            _supporterContext.GameState.LoadPlayer(_supporterContext.IO.LastUserInput);

            _supporterContext.IO.NotifyUser($"Hello, {_supporterContext.GameState.ActivePlayer.Name}!");
            LoadPlayerLocation();
        }

        public void ExitGame(){
            _supporterContext.IO.OutWithSubject("GAME ENGINE", "Attempting shut down ...");
            
            System.Windows.Application.Current.Shutdown();
        }
        
        public void Resume(){
            _supporterContext.ResumeGame();
        }

        public void Delete(){
            _supporterContext.IO.OutWithPrompt("DELETING GAME SAVE","ENTER SAVE TO DELETE");

            string toDelete = _supporterContext.IO.LastUserInput;

            if(!_supporterContext.GameState.GetGameSaves().Contains(toDelete)){
                _supporterContext.IO.OutWithSubject("ERROR", "SAVE DOES NOT EXIST");
                return;
            }

            _supporterContext.IO.OutWithPrompt("ARE YOU SURE?",  "ENTER YES TO CONFIRM");
            
            if(_supporterContext.IO.LastUserInput.ToUpper().Equals("YES")){
                _supporterContext.GameState.DeleteSave(toDelete);
            }
        }

        public override List<string> FinalOptionsProcessing(){
            List<string> excludeList = ["MENU"];
            List<string> final = GlobalMenuOptions(excludeList);

            return final;
        }
        
        public override Dictionary<OptionMap, Action> MapRoute() {
            return new Dictionary<OptionMap, Action>
                {
                    {NEW, () => RetryWorker(()=> NewGame())},
                    {LOAD, () => RetryWorker(()=> LoadGame())},
                    {LOGOUT, () => _supporterContext.GameState.ResetUser()},
                    {EXIT, () => ExitGame()},
                };
        }
    }
}