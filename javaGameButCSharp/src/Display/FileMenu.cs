using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace JavaGameButCSharp{
    public class EngineMenu{
        public Menu MainMenu {get;set;}
        private Dispatcher _dispatcher;
        public RoutedEventHandler converter {get;set;}

        public EngineMenu(Dispatcher dispatcher){
            MainMenu = new ();
            _dispatcher = dispatcher;
        }

        public void LoadMenuStack(string header, Dictionary<OptionMap, Action> MenuStack){
            _dispatcher.Invoke(() =>
            {
                MainMenu.Items.Clear();
                MenuItem menuHeader = new (){ Header = $"_{header}"};

                foreach(OptionMap item in MenuStack.Keys){
                    MenuItem newItem = new() { Header = $"_{item.ToString()}"};
                    newItem.Tag = item;
                    newItem.Click += converter;
                    menuHeader.Items.Add(newItem);
                }

                MainMenu.Items.Add(menuHeader);
            });
        }


    }
}