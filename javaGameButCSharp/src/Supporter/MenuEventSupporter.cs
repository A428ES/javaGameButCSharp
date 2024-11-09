using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class MainMenuEventSupporter(SupporterContext supporterContext) : ISupporter{
        private readonly SupporterContext _supporterContext = supporterContext;
        public void NewGame(){
            _supporterContext.IO.OutWithPrompt("CREATING NEW SAVE", "ENTER YOUR NAME");
            
            _supporterContext.GameState.NewPlayer(_supporterContext.IO.LastUserInput);
            LoadPlayerLocation();
        }

        public void Stats(){
            _supporterContext.IO.OutWithSubject("GAME ENGINE","Loading stats ...");

            _supporterContext.GameState.LoadStats();
        }

        public void LoadPlayerLocation(){
            _supporterContext.SystemEvent = new EventModel(LOCATION_EVENT, _supporterContext.GameState.ActivePlayer.Location);
        }

        public void LoadGame(){
            _supporterContext.IO.OutWithPrompt("LOADING EXISTING GAME", "ENTER YOUR SAVE NAME");
            _supporterContext.GameState.LoadPlayer(_supporterContext.IO.LastUserInput);

            LoadPlayerLocation();
        }

        public void ExitGame(){
            _supporterContext.IO.OutWithSubject("GAME ENGINE", "Attempting shut down ...");
            _supporterContext.SystemEvent = new EventModel(EXIT);
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

        public List<string> GetAllUserOptions(){
            var options = new List<string>(_supporterContext.WorkingEvent.InputOptions);

            if (!string.IsNullOrEmpty(_supporterContext.GameState.ActivePlayer.Name) && !options.Contains("RESUME")){
                options.Add("RESUME");
            }

            return options;
        }

        public void RetryWorker(Action action){
            _supporterContext.RetryHelper.ExecuteWithRetry(action, 5);
        }

        public void Routing() {
            OptionMap enumResult;
            
            _supporterContext.IO.OutWithOptionsPrompt(_supporterContext.WorkingEvent.EventText, GetAllUserOptions());
            Enum.TryParse<OptionMap>(_supporterContext.IO.LastUserInput, true, out enumResult);

            switch(enumResult){
                case EXIT:
                    ExitGame();
                    break;
                case NEW:
                    _supporterContext.RetryHelper.ExecuteWithRetry(()=> NewGame(), 5);
                    break;
                case LOAD:
                    _supporterContext.RetryHelper.ExecuteWithRetry(()=> LoadGame(), 5);
                    break;
                case STATS:
                    Stats();
                    break;
                case RESUME:
                    Resume();
                    break;
                case DELETE:
                    _supporterContext.RetryHelper.ExecuteWithRetry(()=> Delete(), 5);
                    break;
                default:
                    _supporterContext.IO.OutWithSubject("ERROR", "Invalid entry");

                    Routing();
                    break;
            }
        }
    }
}