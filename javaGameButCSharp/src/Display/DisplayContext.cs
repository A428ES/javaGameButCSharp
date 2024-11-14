using System.Windows.Controls;

namespace JavaGameButCSharp{
    public class DisplayContext{
        public EngineMenu GameMenu {get;}
        public Canvas GameCanvas {get;}
 
        public DisplayContext(EngineMenu gameMenu, Canvas gameCanvas){
            GameCanvas = gameCanvas;
            GameMenu = gameMenu;
        }

    }
}