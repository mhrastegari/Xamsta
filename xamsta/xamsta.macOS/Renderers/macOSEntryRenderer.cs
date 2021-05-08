using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using xamsta.macOS.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(macOSEntryRenderer))]
namespace xamsta.macOS.Renderers
{
    public class macOSEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control != null && Control.Layer != null)
            {
                Control.FocusRingType = AppKit.NSFocusRingType.None;
                Control.Layer.BorderWidth = 0;
                Control.Bordered = false;
            }
        }
    }
}
