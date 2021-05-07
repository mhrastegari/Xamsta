using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using xamsta.Droid.Renderers;

[assembly: ExportRenderer(typeof(RefreshView), typeof(AndroidRefreshViewRenderer))]
namespace xamsta.Droid.Renderers
{
    public class AndroidRefreshViewRenderer : RefreshViewRenderer
    {
        public AndroidRefreshViewRenderer(Context context) : base(context)
        {
            MOriginalOffsetTop = 100;
            SetProgressViewOffset(true, 100, 410);
        }
    }
}