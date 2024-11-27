using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace JavaGameButCSharp
{

    class GameRenderer
    {
        private Grid _mainGrid;
        public Grid MainGrid => _mainGrid;
        public Canvas _backgroundCanvas {get;}
        public Canvas _objectCanvas {get;}
        public Canvas _entityCanvas {get;}
        public GameMenu GameMenu {get;}
        public RenderService RenderServiceSupport {get;}
        public GameStatusBar StatusBar;
        public double Height;
        public double Width;
        public int PixelSize;

        public GameRenderer(double height, double width, int pixelSize, Dispatcher dispatcher, GameActivityController activityController){
            _backgroundCanvas = new();
            _objectCanvas = new ();
            _entityCanvas = new ();

            Height = height;
            Width = width;
            PixelSize = pixelSize;

            GameMenu = new(dispatcher);
            StatusBar = new GameStatusBar(dispatcher);
            _mainGrid = new Grid();

            RenderServiceSupport = new(Height, Width, PixelSize, activityController);
            RenderServiceSupport.ActivityController.SetStatusBar(StatusBar);

            GenerateGrid();
        }

        public double WindowHeight(){
            return 653;
        }

        public double WindowWidth(){
            return 814;
        }

        public void GenerateGrid(){
            _mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            _mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            _mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Grid.SetRow(GameMenu.MainMenu, 0); 
            Grid.SetRow(_backgroundCanvas, 1); 
            Grid.SetRow(_objectCanvas, 1); 
            Grid.SetRow(_entityCanvas, 1); 
            Grid.SetRow(StatusBar.StatusBar, 2);

            _mainGrid.Children.Add(GameMenu.MainMenu);
            _mainGrid.Children.Add(_backgroundCanvas);
            _mainGrid.Children.Add(_objectCanvas);
            _mainGrid.Children.Add(_entityCanvas);
            _mainGrid.Children.Add(StatusBar.StatusBar);
        }

        public Sprite RenderPlayerSprite(string imagePath){
            return new Player(imagePath, _entityCanvas, new(100, 100), RenderServiceSupport).RenderSprite(true);;
        }

        public Sprite RenderNPCSprite(string imagePath){
            return new NPC(imagePath, _objectCanvas, new(125, 110), RenderServiceSupport).RenderSprite(true);
        }

        public void DestroySprite(Sprite spriteToDestroy){
            if(spriteToDestroy == null){
                return;
            }

            spriteToDestroy.Remove();
        }
        
        public void RenderObjectMap()
        {
            Sprite houseSpawn = new StationarySprite(imagePath: @"C:\3.png",_objectCanvas, RenderServiceSupport)
                                                    .FrameDimensions(147, 157)
                                                    .SetPosition(new(300, 200))
                                                    .RenderSprite(walkable: false);

            RenderNPCSprite(@"C:\NPCWalk.png");
        }

        public void RenderTileMap()
        {
            int tileWidth = 32;
            int tileHeight = 32;
            int rows = (int)(this.Height / tileHeight);
            int cols = (int)(this.Width / tileWidth);
            
            Sprite backgroundTile = new StationarySprite (@"C:\FieldsTile_01.png", _backgroundCanvas, RenderServiceSupport);
            Sprite borderTile = new StationarySprite (@"C:\Tile2_60.png", _backgroundCanvas, RenderServiceSupport);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    backgroundTile.SetPosition(new(col * tileWidth, row * tileHeight)).RenderSprite();

                    if(RenderServiceSupport.BoundaryService.IsBoundary(col, row)){
                        borderTile.SetPosition(new(col * tileWidth, row * tileHeight)).RenderSprite();
                    }
                }
            }
        }
    }
}