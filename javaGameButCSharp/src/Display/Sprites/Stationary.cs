using System.Windows.Controls;

namespace JavaGameButCSharp{
    class StationarySprite : Sprite{
        public StationarySprite(string imagePath, Canvas canvas, RenderService renderService) : base(imagePath, canvas, renderService){
            FrameDimensions();
        }
    }
}