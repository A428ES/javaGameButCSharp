
using System.Windows.Controls;
using System.Windows.Threading;
using Point = System.Windows.Point;

namespace JavaGameButCSharp{
    class NPC : AnimatedSprite, ISpriteAutoMoveable{
        private DispatcherTimer _autoWalkTimer;
        public static readonly Random RandMove = new Random();

        public NPC(string imagePath, Canvas canvas, Point position, RenderService renderService) : base(imagePath, canvas, position, renderService){
            _autoWalkTimer = new DispatcherTimer();
            _autoWalkTimer.Interval = TimeSpan.FromSeconds(0.15); 
            _autoWalkTimer.Tick += AutoWalker;
            _autoWalkTimer.Start();

            FrameDimensions(42, 42, 6);
        }

        public void AutoWalker(object? sender, EventArgs e)
        {
            if(!_canvas.IsEnabled)
            {
                StopAnimation();
                return;
            }

            int swapAxis = RandMove.Next(0, 25);

            if(swapAxis == 14){
                switch(Math.Abs(WalkDirection)){
                    case 1:
                        WalkDirection = 2;
                        break;
                    case 2:
                        WalkDirection = 1;
                        break;
                }
            }

            MovementRouter(WalkDirection, 10);
        }
    }
}