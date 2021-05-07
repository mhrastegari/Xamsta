using Android.App;
using Xamarin.Forms;
using xamsta.Droid.Renderers;
using xamsta.Helpers;

[assembly: Dependency(typeof(StatusBar))]
namespace xamsta.Droid.Renderers
{
    class StatusBar : IStatusBar
    {
        public static Activity Activity { get; set; }

        public int GetHeight()
        {
            int statusBarHeight = -1;
            int resourceId = Activity.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = Activity.Resources.GetDimensionPixelSize(resourceId);
            }
            return statusBarHeight;
        }
    }
}