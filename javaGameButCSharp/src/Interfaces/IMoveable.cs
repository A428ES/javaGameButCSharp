using Point = System.Windows.Point;

namespace JavaGameButCSharp{
    interface IMoveable{
        void Move(Point newPosition);
        void MovementRouter(int direction, int moveDistance);
        void SpriteOrientation(int x, int y);
    }
}