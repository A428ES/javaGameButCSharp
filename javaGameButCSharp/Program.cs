using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;


namespace JavaGameButCSharp
{
    class MainWindow : Window
    {
        public Engine GameEngine { get; set; }
        public DisplayContext DisplayContext {get; set;}
        public GameRenderer gameRenderer {get;}

        public MainWindow()
        {
            this.Title = "javaGameButCSharp";
            this.Width = 800;
            this.Height = 576;
            
            GameActivityController activityController = new GameActivityController(() => Pause(),() => Unpause());
            gameRenderer = new(this.Height, this.Width, 32, this.Dispatcher, activityController);
            gameRenderer.RenderTileMap();
            gameRenderer.RenderObjectMap();

            DisplayContext = new(gameRenderer);

            this.Content = gameRenderer.MainGrid;
            this.Loaded += MainWindow_Loaded;
            this.SizeChanged += OnWindowSizeChanged;
        }
        
        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            DisplayContext.Renderer.StatusBar.UpdateGameDimensions($"Window Size: {this.ActualWidth:F0} x {this.ActualHeight:F0}");
        }
        private void Unpause(){
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                gameRenderer._backgroundCanvas.IsEnabled = true;
                gameRenderer._objectCanvas.IsEnabled = true;

                gameRenderer._entityCanvas.Focus();
            });
        }

        private void Pause(){
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                gameRenderer._backgroundCanvas.IsEnabled = false;
                gameRenderer._objectCanvas.IsEnabled = false;

                gameRenderer._entityCanvas.Focus();
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Width = DisplayContext.Renderer.WindowWidth(); 
            System.Windows.Application.Current.MainWindow.Height = DisplayContext.Renderer.WindowHeight();

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