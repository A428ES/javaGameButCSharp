using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace JavaGameButCSharp{
    interface KeyControlledObject{
        void OnKeyDown(object sender, KeyEventArgs e);
        void OnKeyUp(object sender, KeyEventArgs e);
    }
}