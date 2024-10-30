using System;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    public class MainGame{
        static void Main(){
            SaveLoadManagement saveLoad = new SaveLoadManagement();
            JsonStateManagement jsonManager = new JsonStateManagement();

            saveLoad.NewSave("NEWGAME");

            string userLoadPath = saveLoad.GetStatePath(ENTITY, "PLAYER");
            Entity player = jsonManager.Read<Entity>(userLoadPath);

            Console.WriteLine(player.Inventory.EntityInventory[0].Type);
        }
    }
}