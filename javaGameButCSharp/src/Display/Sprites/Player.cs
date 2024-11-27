using System.Windows.Controls;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Point = System.Windows.Point;

namespace JavaGameButCSharp{
    class Player : AnimatedSprite, KeyControlledObject{

        public Player(string imagePath, Canvas canvas, Point position, RenderService renderService) : base(imagePath, canvas, position, renderService){
            _canvas.KeyDown += OnKeyDown;
            _canvas.KeyUp += OnKeyUp;
            _canvas.Focusable = true;
            _canvas.Focus();

            FrameDimensions(42, 42, 6);
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.A:
                case Key.S:
                case Key.D:
                    StopAnimation();
                    break;                    
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            int moveDistance = 10;
            int moveDirection = 0;

            switch (e.Key)
            {
                case Key.W:
                    moveDirection = -1;
                    break;
                case Key.S:
                    moveDirection = 1;
                    break;
                case Key.A:
                    moveDirection = -2;
                    break;
                case Key.D:
                    moveDirection = 2;
                    break;
                case Key.Space:
                    RenderServiceSupport.ActivityController.ToggleState();
                    break;
            }
            
            MovementRouter(moveDirection, moveDistance);
        }
    }
}