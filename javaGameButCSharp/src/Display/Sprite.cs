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

        public void DestroySprite(){
            canvas.Children.Remove(imageControl);
        }

        public Sprite(string imagePath, System.Windows.Point initialPosition, Canvas canvas)
        {
            this.canvas = canvas;
            Position = initialPosition;
            ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));

            // Create the Image control to display the sprite
            imageControl = new System.Windows.Controls.Image
            {
                Source = ImageSource,
                Width = ImageSource.Width,
                Height = ImageSource.Height
            };

            // Set initial position and add sprite to canvas
            Canvas.SetLeft(imageControl, Position.X);
            Canvas.SetTop(imageControl, Position.Y);
            canvas.Children.Add(imageControl);

            // Register the KeyDown event handler directly in the Sprite class
            canvas.KeyDown += OnKeyDown;
            canvas.Focusable = true;
            canvas.Focus();
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int moveDistance = 10;

            switch (e.Key)
            {
                case Key.W:
                    Move(0, -moveDistance);
                    break;
                case Key.S:
                    Move(0, moveDistance);
                    break;
                case Key.A:
                    Move(-moveDistance, 0);
                    break;
                case Key.D:
                    Move(moveDistance, 0);
                    break;
            }
        }

        public void Move(int deltaX, int deltaY)
        {
            Position = new System.Windows.Point(Position.X + deltaX, Position.Y + deltaY);
            Canvas.SetLeft(imageControl, Position.X);
            Canvas.SetTop(imageControl, Position.Y);
        }
    }
}