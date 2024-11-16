namespace JavaGameButCSharp{
    public class Boundary{
        private int[,] _boundaryGrid;
        private int _tileSize;
        private int _cols;
        private int _rows;

        public Boundary(double height, double width, int tileSize){
            _tileSize = tileSize;
            _cols = (int)Math.Round(width/tileSize);
            _rows = (int)Math.Round(height/tileSize);

            _boundaryGrid = new int[_rows, _cols];
        }

        public void OccupyGrid(int x, int y, int width, int height){
            int tileX = x/_tileSize;
            int tileY = y/_tileSize;

            int objectTileHeight = height / _tileSize;
            int objectTileWidth = width / _tileSize;

            for (int row = tileY; row < tileY + objectTileHeight; row++) {
                for (int col = tileX; col < tileX + objectTileWidth; col++) {
                    _boundaryGrid[row, col] = 1;
                }
            }
        }

        public bool Walkable(double x, double y){
            int tileX = (int)Math.Round(x/_tileSize);
            int tileY = (int)Math.Round(y/_tileSize);

            if (tileX < 0 || tileY < 0 || tileX >= _cols || tileY >= _rows) {
                return false; 
            }
            
            return _boundaryGrid[tileY, tileX] == 0; 
        }

        public void BuildGrid(){
            for (int row = 0; row < _rows; row++) {
                for (int col = 0; col < _cols; col++) {
                    if (row == 0 || row == _rows-1 || col == 0 || col == _cols-1) {
                        _boundaryGrid[row, col] = 1; 
                    }
                }
            }
        }
    }
}