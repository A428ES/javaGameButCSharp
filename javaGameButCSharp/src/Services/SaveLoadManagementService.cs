using System;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    class SaveLoadManagement{
        public readonly string GamePath;
        public string GameTemplates;
        public string LoadedUserPath;
        public string LoadedUser;

        public SaveLoadManagement(){
            this.GamePath = @"C:\javaGameEvolution";
            this.GameTemplates = $@"{GamePath}\GameSaveTemplate";
            this.LoadedUser = String.Empty;
            this.LoadedUserPath = String.Empty;
        }

        public void LoadUser(string loadUser){
            this.LoadedUser = loadUser;
            this.LoadedUserPath = $@"{GamePath}\GameSaves\{LoadedUser}";
        }

        public string GetStatePath(OptionMap type, string fileName){
            if(type == EVENT){
                return $@"{GamePath}\{type}\{fileName}.json";
            }

            return $@"{LoadedUserPath}\{type}\{fileName}.json";
        }

        public void NewSave(string saveName){
            LoadUser(saveName);
            FileManagement.CopyDirectory(GameTemplates, LoadedUserPath);
        }

        public void DeleteSave(string saveName){
            FileManagement.DeleteDirectory(saveName);
            LoadUser(String.Empty);
        }
    }
}