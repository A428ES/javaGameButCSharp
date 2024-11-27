using Point = System.Windows.Point;

namespace JavaGameButCSharp{
    interface IAnimated{
        void StartAnimation();
        void StopAnimation(bool resetToFirstFrame=false);
        void UpdateFrame(object sender, EventArgs e);
    }
}