using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using xamsta.Droid.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(AndroidEntryRenderer))]

namespace xamsta.Droid.Renderers
{
    public class AndroidEntryRenderer : EntryRenderer
    {
        public AndroidEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}
