using System.Windows.Controls;
using Point = System.Windows.Point;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{

    struct Tile {
        public int X;
        public int Y;
        public int Height;
        public int Width;

        public Tile(Point position, int tileSize = 32, int width=0, int height=0)
        {
            X = (int)Math.Round(position.X / tileSize);
            Y = (int)Math.Round(position.Y / tileSize);
            Width = width > 0 ? width / tileSize : 0;
            Height = height > 0 ? height / tileSize : 0;
        }
    }

    class Boundary{
        private GridItem[,] _boundaryGrid;
        private int _tileSize;
        private int _cols;
        private int _rows;
        public Boundary(double height, double width, int tileSize){
            _tileSize = tileSize;
            _cols = (int)Math.Round(width/tileSize);
            _rows = (int)Math.Round(height/tileSize);

            _boundaryGrid = new GridItem[_rows, _cols];
            
            BuildGrid();
        }

        public bool IsBoundary(double tileX, double tileY){
            if (tileX <= 0 || tileY <= 0 || tileX >= _cols-1 || tileY >= _rows-1) {
                return true; 
            }

            return false;
        }

        public void OccupyGrid(Point position, int width, int height){
            Tile newTile = new(position, _tileSize, width, height);

            for (int row = newTile.Y; row < newTile.Y + newTile.Height; row++) {
                for (int col = newTile.X; col < newTile.X + newTile.Width; col++) {
                    _boundaryGrid[row, col].Walkable = false;
                }
            }
        }

        public OptionMap WalkAndUpdate(Tile nextTile, Tile previousTile, Sprite attemptingSprite){
            GridItem nextGridItem = _boundaryGrid[nextTile.Y, nextTile.X];

            if (nextTile.X < 0 || nextTile.Y < 0 || nextTile.X >= _cols || nextTile.Y >= _rows) {
                return BOUNDARY; 
            }

            if(previousTile.X == nextTile.X && previousTile.Y == nextTile.Y){
                return NOCHANGE;
            }

            if(nextGridItem.OccupyingSprite != null && nextGridItem.OccupyingSprite == attemptingSprite ){
                return NOCHANGE;
            }
            
            if(nextGridItem.OccupyingSprite != null && nextGridItem.OccupyingSprite != attemptingSprite ){
                return INTERSECTION;
            }
            
            return nextGridItem.Walkable ? WALKABLE : OCCUPIED; 
        }

        public void UpdatePosition(Tile nextTile, Tile previousTile, Sprite attemptingSprite){
            GridItem nextGridItem = _boundaryGrid[nextTile.Y, nextTile.X];
            GridItem lastGridItem  = _boundaryGrid[previousTile.Y, previousTile.X];

            nextGridItem.Walkable = false;
            nextGridItem.OccupyingSprite = attemptingSprite;

            lastGridItem.Walkable = true;
            lastGridItem.OccupyingSprite = null;
        }
    

        public void BuildGrid(){
            for (int row = 0; row < _rows; row++) {
                for (int col = 0; col < _cols; col++) {
                    if (row == 0 || row == _rows-1 || col == 0 || col == _cols-1) {
                        _boundaryGrid[row, col] = new GridItem(false, null);
                    } else {
                        _boundaryGrid[row, col] = new GridItem(true, null);
                    }
                }
            }
        }
    }
}