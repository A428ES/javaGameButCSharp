using System.IO;
using System.Text.Json.Serialization;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class SaveLoadManagement : StatefulObject{
        [JsonPropertyName("gamepath")]
        public string GamePath {get;set;} = string.Empty;
        [JsonPropertyName("templates")]
        public string GameTemplates {get;set;} = string.Empty;
        [JsonPropertyName("extension")]
        public string Extension {get;set;} = string.Empty;
        public string LoadedUserPath = string.Empty;
        public string LoadedUser = string.Empty;

        public SaveLoadManagement(){
        }

        public void LoadConfig(){
            GameTemplates = Path.Combine(GamePath, GameTemplates);
        }

        public void LoadUser(string loadUser){
            this.LoadedUser = loadUser;
            this.LoadedUserPath = $@"{GamePath}\GameSaves\{LoadedUser}";
        }

        public string GetStatePath(OptionMap type, string fileName){
            if(type == EVENT){
                return $@"{GamePath}\{type}\{fileName.ToUpper()}.{Extension}";
            }

            return $@"{LoadedUserPath}\{type}\{fileName.ToUpper()}.{Extension}";
        }

        public void NewSave(string saveName){
            if(saveName.Equals(string.Empty)){
                throw new InvalidInput("Failure to recognize user input");
            }

            LoadUser(saveName);

            if(Directory.Exists(LoadedUserPath)){
                throw new InvalidInput("This save already exists");
            }

            FileManagement.CopyDirectory(GameTemplates, LoadedUserPath);
        }

        public void DeleteSave(string saveName){
            try{
                Directory.Delete($@"{GamePath}\GameSaves\{saveName}", recursive: true);
                
                LoadUser(String.Empty);
            } catch (Exception e){
                throw new ResourceNotFound($"Unable to delete directory: {e.Message}");
            }
        }

        public List<String> GetSaveGameList(){
            return Directory.GetDirectories($@"{GamePath}\GameSaves")
                                .Select(Path.GetFileName).ToList();

        }
    }
}