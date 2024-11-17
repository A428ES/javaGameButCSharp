using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace JavaGameButCSharp
{
    class MainWindow : Window
    {
        public Engine GameEngine { get; set; }
        public DisplayContext DisplayContext {get; set;}

        public MainWindow()
        {
            this.Title = "javaGameButCSharp";
            this.Width = 800;
            this.Height = 576;
            
            GameRenderer gameRenderer = new(this.Height, this.Width, 32, this.Dispatcher);

            gameRenderer.RenderTileMap();
            gameRenderer.RenderObjectMap();

            DisplayContext = new(gameRenderer);

            this.Content = gameRenderer.mainGrid;
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GameEngine = new Engine(@"C:\javaGameEvolution\game.json", DisplayContext);
            Task.Run(() => GameEngine.EngineLoop());
        }


        [STAThread]
        public static void Main()
        {
            System.Windows.Application app = new System.Windows.Application();
            app.Run(new MainWindow());
        }
    }
}