using Point = System.Windows.Point;

namespace JavaGameButCSharp{
    interface ISpriteMovement{
        void StartAnimation();
        void StopAnimation(bool resetToFirstFrame=false);
        void UpdateFrame(object sender, EventArgs e);
        void Move(Point newPosition);
        void MovementRouter(int direction, int moveDistance);
        void SpriteOrientation(int x, int y);
    }
}