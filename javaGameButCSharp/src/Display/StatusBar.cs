using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace JavaGameButCSharp{
    class GameStatusBar{
        public StatusBar StatusBar {get;}
        private StatusBarItem _playerStats;
        private StatusBarItem _pauseStatus;
        private StatusBarItem _gameDimensions;
        private Dispatcher _dispatcher;
        public GameStatusBar(Dispatcher dispatcher){
            StatusBar = new StatusBar();

            _dispatcher = dispatcher;
            _gameDimensions = new StatusBarItem();
            _playerStats = new StatusBarItem();
            _pauseStatus = new StatusBarItem();

            StatusBar.Items.Add(_gameDimensions);
            StatusBar.Items.Add(new Separator());
            StatusBar.Items.Add(_playerStats);
            StatusBar.Items.Add(new Separator());
            StatusBar.Items.Add(_pauseStatus);
        }

        public void UpdatePlayerStats(string message){
            _dispatcher.Invoke(() =>
            {
                _playerStats.Content = message;
            });
        }

        public void UpdateGameDimensions(string message){
            _dispatcher.Invoke(() =>
            {
                _gameDimensions.Content = message;
            });
        }

        public void UpdatePauseStatus(string message){
            _dispatcher.Invoke(() =>
            {
                _pauseStatus.Content = message;
            });
        }
    }
}