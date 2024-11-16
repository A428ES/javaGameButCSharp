using System.Windows.Controls;

namespace JavaGameButCSharp{
    public class DisplayContext{
        public EngineMenu GameMenu {get;}
        public Canvas GameCanvas {get;}
        public Boundary Boundary {get;}
 
        public DisplayContext(EngineMenu gameMenu, Canvas gameCanvas, Boundary boundary){
            GameCanvas = gameCanvas;
            GameMenu = gameMenu;
            Boundary = boundary;
        }

    }
}