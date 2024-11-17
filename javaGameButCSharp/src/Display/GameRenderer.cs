using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace JavaGameButCSharp
{

    class GameRenderer
    {
        public Canvas _backgroundCanvas {get;}
        public Canvas _objectCanvas {get;}
        public Canvas _entityCanvas {get;}
        public GameMenu GameMenu {get;}
        public Boundary _boundaryHandler {get;set;}
        public Grid mainGrid;
        public double Height;
        public double Width;
        public int PixelSize;

        public GameRenderer(double height, double width, int pixelSize, Dispatcher dispatcher){
            Height = height;
            Width = width;
            PixelSize = pixelSize;
            _backgroundCanvas = new();
            _objectCanvas = new ();
            _entityCanvas = new ();
            GameMenu = new(dispatcher);

            GenerateGrid();
            GenerateBoundary();
        }

        public void GenerateBoundary(){
            _boundaryHandler = new(Height, Width, PixelSize);
            _boundaryHandler.BuildGrid();
        }

        public void GenerateGrid(){
            mainGrid = new Grid();

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Grid.SetRow(GameMenu.MainMenu, 0); 
            Grid.SetRow(_backgroundCanvas, 1); 
            Grid.SetRow(_objectCanvas, 1); 
            Grid.SetRow(_entityCanvas, 1); 

            mainGrid.Children.Add(GameMenu.MainMenu);
            mainGrid.Children.Add(_backgroundCanvas);
            mainGrid.Children.Add(_objectCanvas);
            mainGrid.Children.Add(_entityCanvas);
        }

        public Sprite RenderPlayerSprite(string imagePath){
                return new(
                    imagePath,
                    new System.Windows.Point(100, 100),
                    _entityCanvas,
                    _boundaryHandler,
                    frameCount: 6,
                    frameWidth: 42,
                    frameHeight: 42
                );
        }

        public void DestroySprite(Sprite spriteToDestroy){
            if(spriteToDestroy == null){
                return;
            }
            
            spriteToDestroy.DestroySprite();
        }

        public void RenderObjectMap()
        {
            System.Windows.Controls.Image theHouse = new System.Windows.Controls.Image
            {
                Source = new BitmapImage(new Uri(@"C:\3.png")),
                Width = 147,
                Height = 157
            };

            _boundaryHandler.OccupyGrid(300, 200, 147, 157);

            Canvas.SetLeft(theHouse, 300);
            Canvas.SetTop(theHouse, 200);
            _objectCanvas.Children.Add(theHouse);
        }

        public void RenderTileMap()
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

                    if(_boundaryHandler.IsBoundary(col, row)){
                        System.Windows.Controls.Image borderObject = new System.Windows.Controls.Image
                        {
                            Source = new BitmapImage(new Uri(@"C:\Tile2_60.png")),
                            Width = tileWidth,
                            Height = tileHeight
                        };

                        Canvas.SetLeft(borderObject, col * tileWidth);
                        Canvas.SetTop(borderObject, row * tileHeight);

                        _objectCanvas.Children.Add(borderObject);
                    }


                    Canvas.SetLeft(tileImage, col * tileWidth);
                    Canvas.SetTop(tileImage, row * tileHeight);
                    _backgroundCanvas.Children.Add(tileImage);
                }
            }
        }
    }
}