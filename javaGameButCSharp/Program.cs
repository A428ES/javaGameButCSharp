namespace JavaGameButCSharp{
    public class MainGame{
        static void Main(){
            Engine gameEngine = new Engine();

            gameEngine.NewPlayer("MyNewGame");
            gameEngine.EngineLoop();
        }
    }
}