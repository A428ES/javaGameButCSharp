using System.Windows.Controls.Primitives;

namespace JavaGameButCSharp{
    class GameActivityController{
        public bool IsActive {get; private set;}
        private Action _unpause;
        private Action _pause;
        private GameStatusBar StatusBar;

        public GameActivityController(Action pause, Action unpause){
            IsActive = false;
            _pause = pause;
            _unpause = unpause;
        }

        public void SetStatusBar(GameStatusBar statusBar){
            StatusBar = statusBar;
        }

        public void UpdateStatusBar(string msg){
            if(StatusBar == null){
                return;
            }

            StatusBar.UpdatePauseStatus(msg);
        }

        public void PauseGame(){
            IsActive = false;
            _pause();
            UpdateStatusBar("GAME PAUSED");
        }

        public void UnPauseGame(){
            IsActive = true;
            _unpause();
            UpdateStatusBar("GAME ACTIVE");
        }

        public void ToggleState(){
            IsActive = !IsActive;

            switch(IsActive){
                case true:
                    UnPauseGame();
                    break;
                case false:
                default:
                    PauseGame();
                    break;
            }
        }
    }
}