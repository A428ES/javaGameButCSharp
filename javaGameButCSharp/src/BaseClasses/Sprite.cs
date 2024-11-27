using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;

namespace JavaGameButCSharp
{
    abstract class Sprite
    {
        public Canvas _canvas {get;}
        public Image ImageControl {get;set;} = new Image();
        public BitmapImage ImageSource { get; private set; }
        public Point Position { get; set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public int FrameCount;
        public int CurrentFrame;
        public RenderService RenderServiceSupport {get;}
        private const int DefaultFrameWidth = 32;
        private const int DefaultFrameHeight = 32;

        public Sprite(string imagePath, Canvas canvas, RenderService renderService)
        {
            RenderServiceSupport = renderService;
            _canvas = canvas;
            ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
        }

        public Sprite FrameDimensions(int frameWidth = DefaultFrameWidth, int frameHeight = DefaultFrameHeight, int frameCount = 1){
            FrameCount = frameCount;
            FrameWidth = frameWidth > 0 ? frameWidth : (int)ImageSource.PixelWidth / frameCount;
            FrameHeight = frameHeight > 0 ? frameHeight : (int)ImageSource.PixelHeight;

            return this;
        }

        public void CreateImage(bool cropped=false){
            ImageControl = new Image { Source=ImageSource, Width=FrameWidth, Height=FrameHeight};

            if(cropped){
                ImageControl.Source = new CroppedBitmap(ImageSource, new Int32Rect(0, 0, FrameWidth, FrameHeight));
            }
        }

        public Sprite SetPosition(Point newPosition){
            Position = newPosition;

            return this;
        }

        public Sprite RenderSprite(bool crop=false, bool walkable=true){
            CreateImage(crop);

            Canvas.SetLeft(ImageControl, Position.X);
            Canvas.SetTop(ImageControl, Position.Y);
            _canvas.Children.Add(ImageControl);

            if(!walkable){
                RenderServiceSupport.BoundaryService.OccupyGrid(Position,  FrameWidth, FrameHeight);
            }

            return this;
        }

        public void Remove(){

        }
    }
}