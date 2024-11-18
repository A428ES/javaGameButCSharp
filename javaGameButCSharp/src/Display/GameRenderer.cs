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
                return new Sprite(imagePath, _entityCanvas, _boundaryHandler)
                                .SetPosition(100, 100)
                                .FrameDimensions(42, 42, 6)
                                .AttachAnimation()
                                .AttachKeyControl()
                                .RenderSprite(true);
        }

        public void DestroySprite(Sprite spriteToDestroy){
            if(spriteToDestroy == null){
                return;
            }
            
            spriteToDestroy.DestroySprite();
        }
        
        public void RenderObjectMap()
        {
            Sprite houseObject = new Sprite(imagePath: @"C:\3.png",_objectCanvas,_boundaryHandler)
                                            .SetPosition(300, 200)
                                            .FrameDimensions(147, 157)
                                            .RenderSprite();

            _boundaryHandler.OccupyGrid(300, 200, 147, 157);
        }

        public void RenderTileMap()
        {
            int tileWidth = 32;
            int tileHeight = 32;
            int rows = (int)(this.Height / tileHeight);
            int cols = (int)(this.Width / tileWidth);
            Sprite backgroundTile = new Sprite(@"C:\FieldsTile_01.png", _backgroundCanvas, _boundaryHandler);
            Sprite borderTile = new Sprite(@"C:\Tile2_60.png", _objectCanvas, _boundaryHandler);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    backgroundTile.SetPosition(col * tileWidth, row * tileHeight).RenderSprite();

                    if(_boundaryHandler.IsBoundary(col, row)){
                        borderTile.SetPosition(col * tileWidth, row * tileHeight).RenderSprite();
                    }
                }
            }
        }
    }
}