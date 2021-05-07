using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using xamsta.UWP.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(UWPEntryRenderer))]
namespace xamsta.UWP.Renderers
{
    public class UWPEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
                Control.Padding = new Windows.UI.Xaml.Thickness(1, 6, 0, 6);
            }

        }
    }
}
