using System.Windows;
using System.Windows.Controls;


namespace JavaGameButCSharp
{
    public class MainWindow : Window
    {
        public Engine GameEngine { get; set; }
        public EngineMenu EngineMenu { get; set; }
        public DisplayContext DisplayContext {get; set;}
        private Sprite PlayerSprite;

        public MainWindow()
        {
            this.Title = "javaGameButCSharp";
            this.Width = 800;
            this.Height = 600;

            var mainGrid = new Grid();
            this.Content = mainGrid;

            // Define two rows: one for the menu, one for the game area
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Menu row
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Game area row

            // Create the game area (canvas) and add it to the grid
            DisplayContext = new(new EngineMenu(this.Dispatcher), new Canvas { Background = System.Windows.Media.Brushes.LightGray });

            Grid.SetRow(DisplayContext.GameMenu.MainMenu, 0); // Place EngineMenu in the first row
            mainGrid.Children.Add(DisplayContext.GameMenu.MainMenu);

            Grid.SetRow(DisplayContext.GameCanvas, 1); // Place canvas in the second row
            mainGrid.Children.Add(DisplayContext.GameCanvas);

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize the GameEngine and start its loop after the UI is fully loaded
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