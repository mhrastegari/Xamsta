using UIKit;
using Xamarin.Forms;
using xamsta.Helpers;
using xamsta.iOS.Renderers;

[assembly: Dependency(typeof(StatusBar))]
namespace xamsta.iOS.Renderers
{
    class StatusBar : IStatusBar
    {
        public int GetHeight()
        {
            return (int)UIApplication.SharedApplication.StatusBarFrame.Height;
        }
    }
}