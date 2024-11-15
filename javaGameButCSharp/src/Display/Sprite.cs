using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JavaGameButCSharp
{
    public class Sprite
    {
        public BitmapImage ImageSource { get; private set; }
        public System.Windows.Point Position { get; private set; }
        private Canvas canvas;
        private System.Windows.Controls.Image imageControl;

        // Animation properties
        private int frameWidth;
        private int frameHeight;
        private int frameCount;
        private int currentFrame;
        private System.Windows.Threading.DispatcherTimer animationTimer;

        public void DestroySprite()
        {
            canvas.Children.Remove(imageControl);
        }

        public Sprite(string imagePath, System.Windows.Point initialPosition, Canvas canvas, int frameCount = 1, int frameWidth = 0, int frameHeight = 0)
        {
            this.canvas = canvas;
            Position = initialPosition;
            this.frameCount = frameCount;
            this.frameWidth = frameWidth > 0 ? frameWidth : (int)new BitmapImage(new Uri(imagePath, UriKind.Absolute)).PixelWidth / frameCount;
            this.frameHeight = frameHeight > 0 ? frameHeight : (int)new BitmapImage(new Uri(imagePath, UriKind.Absolute)).PixelHeight;

            ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));

            // Create the Image control to display the sprite
            imageControl = new System.Windows.Controls.Image
            {
                Source = new CroppedBitmap(ImageSource, new Int32Rect(0, 0, this.frameWidth, this.frameHeight)),
                Width = this.frameWidth,
                Height = this.frameHeight
            };

            // Set initial position and add sprite to canvas
            Canvas.SetLeft(imageControl, Position.X);
            Canvas.SetTop(imageControl, Position.Y);
            canvas.Children.Add(imageControl);

            // Initialize animation timer
            animationTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100) // Adjust frame timing
            };
            animationTimer.Tick += UpdateFrame;

            // Register the KeyDown event handler directly in the Sprite class
            canvas.KeyDown += OnKeyDown;
            canvas.KeyUp += OnKeyUp;
            canvas.Focusable = true;
            canvas.Focus();
        }

        private void OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
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

        private void SpriteOrientation(int x, int y){

            var flipTransform = new ScaleTransform
            {
                ScaleX = x,
                ScaleY = y,  
            };

            imageControl.RenderTransform = flipTransform;
            imageControl.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5); 
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int moveDistance = 2;

            switch (e.Key)
            {
                case Key.W:
                    StartAnimation();
                    Move(0, -moveDistance);
                    break;
                case Key.S:
                    StartAnimation();
                    Move(0, moveDistance);
                    break;
                case Key.A:
                    SpriteOrientation(-1, 1);
                    StartAnimation();
                    Move(-moveDistance, 0);
                    break;
                case Key.D:
                    SpriteOrientation(1, 1);
                    StartAnimation();
                    Move(moveDistance, 0);
                    break;
            }
        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            currentFrame = (currentFrame + 1) % frameCount; // Loop through frames
            int xOffset = currentFrame * frameWidth;

            // Update the displayed frame
            imageControl.Source = new CroppedBitmap(ImageSource, new Int32Rect(xOffset, 0, frameWidth, frameHeight));
        }

        public void StartAnimation()
        {
            if (!animationTimer.IsEnabled)
                animationTimer.Start();
        }

        public void StopAnimation()
        {
            if (animationTimer.IsEnabled)
                animationTimer.Stop();
            currentFrame = 0; // Reset to the first frame
            imageControl.Source = new CroppedBitmap(ImageSource, new Int32Rect(0, 0, frameWidth, frameHeight));
        }

        public void Move(int deltaX, int deltaY)
        {
            Position = new System.Windows.Point(Position.X + deltaX, Position.Y + deltaY);
            Canvas.SetLeft(imageControl, Position.X);
            Canvas.SetTop(imageControl, Position.Y);
        }
    }
}