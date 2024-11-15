using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace JavaGameButCSharp
{
    public class MainWindow : Window
    {
        public Engine GameEngine { get; set; }
        public EngineMenu EngineMenu { get; set; }
        public DisplayContext DisplayContext {get; set;}

        public MainWindow()
        {
            this.Title = "javaGameButCSharp";
            this.Width = 800;
            this.Height = 600;

            var mainGrid = new Grid();
            this.Content = mainGrid;

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Menu row
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Game area row/*

            DisplayContext = new(new EngineMenu(this.Dispatcher), new Canvas());

            Grid.SetRow(DisplayContext.GameMenu.MainMenu, 0); 

            Grid.SetRow(DisplayContext.GameCanvas, 1); 
            mainGrid.Children.Add(DisplayContext.GameMenu.MainMenu);


            Canvas tileLayer = new Canvas { Background = System.Windows.Media.Brushes.LightGray };
            Canvas objectLayer = new Canvas();
            Grid.SetRow(tileLayer, 1); 

            mainGrid.Children.Add(tileLayer);
            mainGrid.Children.Add(objectLayer);
            mainGrid.Children.Add(DisplayContext.GameCanvas);
    
            RenderTileMap(tileLayer);
            RenderObjectMap(objectLayer);

        
            this.Loaded += MainWindow_Loaded;
        }

        private void RenderObjectMap(Canvas objectLayer)
        {
            System.Windows.Controls.Image theHouse = new System.Windows.Controls.Image
            {
                Source = new BitmapImage(new Uri(@"C:\3.png")),
                Width = 147,
                Height = 157
            };

            Canvas.SetLeft(theHouse, 300);
            Canvas.SetTop(theHouse, 400);
            objectLayer.Children.Add(theHouse);
        }
        private void RenderTileMap(Canvas tileLayer)
        {
            int tileWidth = 32;
            int tileHeight = 32;
            int rows = (int)(this.Height / tileHeight);
            int cols = (int)(this.Width / tileWidth);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    System.Windows.Controls.Image tileImage = new System.Windows.Controls.Image
                    {
                        Source = new BitmapImage(new Uri(@"C:\FieldsTile_01.png")),
                        Width = tileWidth,
                        Height = tileHeight
                    };

                    Canvas.SetLeft(tileImage, col * tileWidth);
                    Canvas.SetTop(tileImage, row * tileHeight);
                    tileLayer.Children.Add(tileImage);
                }
            }
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