using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using xamsta.macOS.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(macOSEntryRenderer))]
namespace xamsta.macOS.Renderers
{
    public class macOSEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
                
            Control.Layer.BorderWidth = 0;
            Control.Bordered = false;
        }
    }
}
