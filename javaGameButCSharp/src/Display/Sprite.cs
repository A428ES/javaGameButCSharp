using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace JavaGameButCSharp
{
    class Sprite
    {
        private BitmapImage _imageSource { get; }
        private Point _position { get; set; }
        private Canvas _canvas;
        private Image _imageControl;
        private Boundary _boundary;
        private int _frameWidth;
        private int _frameHeight;
        private int _frameCount;
        private int _currentFrame;
        private DispatcherTimer _animationTimer;
        private DispatcherTimer _autoWalkTimer;
        private int _autoWalkDirection = 1;
        public GameActivityController _activityController {get; private set;}
        private static readonly Random RandMove = new Random();

        public Sprite(string imagePath, Canvas canvas, Boundary boundary, GameActivityController activityController)
        {
            _canvas = canvas;
            _boundary = boundary;
            _imageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            _activityController = activityController;
            
            FrameDimensions();
        }
        public void DestroySprite()
        {
            _canvas.Children.Remove(_imageControl);
        }

        public Sprite FrameDimensions(int frameWidth = 32, int frameHeight = 32, int frameCount = 1){
            _frameCount = frameCount;
            _frameWidth = frameWidth > 0 ? frameWidth : (int)_imageSource.PixelWidth / frameCount;
            _frameHeight = frameHeight > 0 ? frameHeight : (int)_imageSource.PixelHeight;

            return this;
        }

        public void CreateImage(bool cropped=false){
            _imageControl = new Image
                {
                    Source = _imageSource,
                    Width = _frameWidth,
                    Height = _frameHeight
                };

            if(cropped){
                _imageControl.Source = new CroppedBitmap(_imageSource, new Int32Rect(0, 0, _frameWidth, _frameHeight));
            }
        }

        public Sprite SetPosition(int x, int y){
            _position = new Point(x, y);

            return this;
        }

        public Sprite RenderSprite(bool crop=false){
            CreateImage(crop);

            Canvas.SetLeft(_imageControl, _position.X);
            Canvas.SetTop(_imageControl, _position.Y);
            _canvas.Children.Add(_imageControl);

            return this;
        }

        public Sprite AttachAnimation()
        {
            _animationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100) 
            };

            _animationTimer.Tick += UpdateFrame;
            
            return this;
        }

        public Sprite AttachKeyControl()
        {
            _canvas.KeyDown += OnKeyDown;
            _canvas.KeyUp += OnKeyUp;
            _canvas.Focusable = true;
            _canvas.Focus();

            return this;
        }

        public void AutoWalker(object sender, EventArgs e)
        {
            if(!_canvas.IsEnabled)
            {
                StopAnimation();
                return;
            }

            int swapAxis = RandMove.Next(0, 25);

            if(swapAxis == 14){
                switch(Math.Abs(_autoWalkDirection)){
                    case 1:
                        _autoWalkDirection = 2;
                        break;
                    case 2:
                        _autoWalkDirection = 1;
                        break;
                }
            }

            MovementRouter(_autoWalkDirection, 10);
        }

        public Sprite AttachAutoWalker()
        {
            _autoWalkTimer = new DispatcherTimer();
            _autoWalkTimer.Interval = TimeSpan.FromSeconds(0.15); // Set interval to 1000ms (1 second)
            _autoWalkTimer.Tick += AutoWalker;
            _autoWalkTimer.Start();

            return this;
        }

        private void SpriteOrientation(int x, int y)
        {
            var flipTransform = new ScaleTransform
            {
                ScaleX = x,
                ScaleY = y,
            };

            _imageControl.RenderTransform = flipTransform;
            _imageControl.RenderTransformOrigin = new Point(0.5, 0.5); 
        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            _currentFrame = (_currentFrame + 1) % _frameCount; 
            int xOffset = _currentFrame * _frameWidth;

            _imageControl.Source = new CroppedBitmap(_imageSource, new Int32Rect(xOffset, 0, _frameWidth, _frameHeight));
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
                _currentFrame = 0;
                _imageControl.Source = new CroppedBitmap(_imageSource, new Int32Rect(0, 0, _frameWidth, _frameHeight));
            }
        }

        public void Move(int deltaX, int deltaY)
        {
            Point NewPosition = new Point(_position.X + deltaX, _position.Y + deltaY);

            if(_boundary.Walkable(NewPosition.X, NewPosition.Y)){
                Canvas.SetLeft(_imageControl, NewPosition.X);
                Canvas.SetTop(_imageControl, NewPosition.Y);

                _position = NewPosition;
            } else {
                _autoWalkDirection *= -1;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
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

        public void MovementRouter(int direction, int moveDistance){
            if(!_activityController.IsActive){
                return;
            }
            
            switch (direction)
            {
                case -1:
                    StartAnimation();
                    Move(0, -moveDistance);
                    break;
                case 1:
                    StartAnimation();
                    Move(0, moveDistance);
                    break;
                case -2:
                    SpriteOrientation(-1, 1);
                    StartAnimation();
                    Move(-moveDistance, 0);
                    break;
                case 2:
                    SpriteOrientation(1, 1);
                    StartAnimation();
                    Move(moveDistance, 0);
                    break;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            int moveDistance = 10;

            switch (e.Key)
            {
                case Key.W:
                    MovementRouter(-1, moveDistance);
                    break;
                case Key.S:
                    MovementRouter(1, moveDistance);
                    break;
                case Key.A:
                    MovementRouter(-2, moveDistance);
                    break;
                case Key.D:
                    MovementRouter(2, moveDistance);
                    break;
                case Key.Space:
                    _activityController.ToggleState();
                    break;

            }
        }
    }
}