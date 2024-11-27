namespace JavaGameButCSharp{
    class GridItem{
        public bool Walkable;
        public Sprite? OccupyingSprite;

        public GridItem(bool walkable, Sprite? sprite){
            Walkable = walkable;
            OccupyingSprite = sprite;
        }
    }
}