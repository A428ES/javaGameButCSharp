namespace JavaGameButCSharp{
    class RenderService{
        public Boundary BoundaryService {get;}
        public GameActivityController ActivityController {get;}
        public InputOutManager IOManager {get;}

        public RenderService(double height, double width, int pixelSize, GameActivityController activityController){
            BoundaryService = new(height, width, pixelSize);
            ActivityController = activityController;
            IOManager = new InputOutManager();
        }
    }
}