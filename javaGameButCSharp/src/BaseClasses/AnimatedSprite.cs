using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Point = System.Windows.Point;
using static JavaGameButCSharp.OptionMap;

namespace JavaGameButCSharp{
    abstract class AnimatedSprite : Sprite, IMoveable, IAnimated{
        private DispatcherTimer _animationTimer;
        public int WalkDirection {get; set;} = 1;

        public AnimatedSprite(string imagePath, Canvas canvas, Point position, RenderService renderService) : base(imagePath, canvas, renderService){
            _animationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100) 
            };

            _animationTimer.Tick += UpdateFrame;

            SetPosition(position);
        }

        public void StartAnimation()
        {
            if (!_animationTimer.IsEnabled)
                _animationTimer.Start();
        }

        public void StopAnimation(bool resetToFirstFrame=false)
        {
            if (_animationTimer.IsEnabled)
                _animationTimer.Stop();
            
            if (resetToFirstFrame){
                CurrentFrame = 0;
                ImageControl.Source = new CroppedBitmap(ImageSource, new Int32Rect(0, 0, FrameWidth, FrameHeight));
            }
        }

        public void Move(Point newPosition)
        {
            Point ProposedPosition = new Point(Position.X + newPosition.X, Position.Y + newPosition.Y);
            Tile previousTile = new(Position);
            Tile nextTile = new(ProposedPosition);

            OptionMap attemptOutcome = RenderServiceSupport.BoundaryService.WalkAndUpdate(nextTile, previousTile, this);
            
            switch(attemptOutcome){
                case WALKABLE:
                case NOCHANGE:
                    Canvas.SetLeft(ImageControl, ProposedPosition.X);
                    Canvas.SetTop(ImageControl, ProposedPosition.Y);
                    if(attemptOutcome == OptionMap.WALKABLE){
                        RenderServiceSupport.BoundaryService.UpdatePosition(nextTile, previousTile, this);
                    }

                    Position = ProposedPosition;

                    break;
                case INTERSECTION:
                    RenderServiceSupport.ActivityController.ToggleState();
                    break;
                case BOUNDARY:
                    WalkDirection *= -1;
                    break;

            }
        }

        public void UpdateFrame(object? sender, EventArgs e)
        {
            CurrentFrame = (CurrentFrame + 1) % FrameCount; 
            int xOffset = CurrentFrame * FrameWidth;

            ImageControl.Source = new CroppedBitmap(ImageSource, new Int32Rect(xOffset, 0, FrameWidth, FrameHeight));
        }

        public void MovementRouter(int direction, int moveDistance){   
            if(!RenderServiceSupport.ActivityController.IsActive){
                return;
            }

            switch (direction)
            {
                case -1:
                    StartAnimation();
                    Move(new Point(0, -moveDistance));
                    break;
                case 1:
                    StartAnimation();
                    Move(new Point(0, moveDistance));
                    break;
                case -2:
                    SpriteOrientation(-1, 1);
                    StartAnimation();
                    Move(new Point(-moveDistance, 0));
                    break;
                case 2:
                    SpriteOrientation(1, 1);
                    StartAnimation();
                    Move(new Point(moveDistance, 0));
                    break;
            }
        }

        public void SpriteOrientation(int x, int y)
        {
            var flipTransform = new ScaleTransform
            {
                ScaleX = x,
                ScaleY = y,
            };

            ImageControl.RenderTransform = flipTransform;
            ImageControl.RenderTransformOrigin = new Point(0.5, 0.5); 
        }
    }
}