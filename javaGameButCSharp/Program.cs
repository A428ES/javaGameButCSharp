namespace JavaGameButCSharp{
    public class MainGame{
        static void Main(){
            Engine gameEngine = new Engine(@"C:\javaGameEvolution\game.json");
            gameEngine.EngineLoop();
        }
    }
}