using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

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
        public GameStatusBar StatusBar;
        public GameActivityController ActivityController;

        public GameRenderer(double height, double width, int pixelSize, Dispatcher dispatcher, GameActivityController activityController){
            _backgroundCanvas = new();
            _objectCanvas = new ();
            _entityCanvas = new ();

            Height = height;
            Width = width;
            PixelSize = pixelSize;
            GameMenu = new(dispatcher);
            StatusBar = new GameStatusBar(dispatcher);
            ActivityController = activityController;

            ActivityController.SetStatusBar(StatusBar);
            GenerateGrid();
            GenerateBoundary();
        }

        public double WindowHeight(){
            return 653;
        }

        public double WindowWidth(){
            return 814;
        }

        public void GenerateBoundary(){
            _boundaryHandler = new(Height, Width, PixelSize);
            _boundaryHandler.BuildGrid();
        }

        public void GenerateGrid(){
            mainGrid = new Grid();

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Grid.SetRow(GameMenu.MainMenu, 0); 
            Grid.SetRow(_backgroundCanvas, 1); 
            Grid.SetRow(_objectCanvas, 1); 
            Grid.SetRow(_entityCanvas, 1); 
            Grid.SetRow(StatusBar.StatusBar, 2);

            mainGrid.Children.Add(GameMenu.MainMenu);
            mainGrid.Children.Add(_backgroundCanvas);
            mainGrid.Children.Add(_objectCanvas);
            mainGrid.Children.Add(_entityCanvas);
            mainGrid.Children.Add(StatusBar.StatusBar);
        }

        public Sprite RenderPlayerSprite(string imagePath){
                return new Sprite(imagePath, _entityCanvas, _boundaryHandler, ActivityController)
                                .SetPosition(100, 100)
                                .FrameDimensions(42, 42, 6)
                                .AttachAnimation()
                                .AttachKeyControl()
                                .RenderSprite(true);
        }

        public Sprite RenderNPCSprite(string imagePath){
            return new Sprite(imagePath, _objectCanvas, _boundaryHandler, ActivityController)
                            .SetPosition(400, 400)
                            .FrameDimensions(42, 42, 6)
                            .AttachAnimation()
                            .AttachAutoWalker()
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
            Sprite houseObject = new Sprite(imagePath: @"C:\3.png",_objectCanvas,_boundaryHandler, ActivityController)
                                            .SetPosition(300, 200)
                                            .FrameDimensions(147, 157)
                                            .RenderSprite();

            RenderNPCSprite(@"C:\NPCWalk.png");

            _boundaryHandler.OccupyGrid(300, 200, 147, 157);
        }

        public void RenderTileMap()
        {
            int tileWidth = 32;
            int tileHeight = 32;
            int rows = (int)(this.Height / tileHeight);
            int cols = (int)(this.Width / tileWidth);
            Sprite backgroundTile = new Sprite(@"C:\FieldsTile_01.png", _backgroundCanvas, _boundaryHandler, ActivityController);
            Sprite borderTile = new Sprite(@"C:\Tile2_60.png", _objectCanvas, _boundaryHandler, ActivityController);

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