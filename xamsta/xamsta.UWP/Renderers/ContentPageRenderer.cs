using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Microsoft.UI.Xaml.Media;
using Xamarin.Forms;
using xamsta.UWP.Renderers;

[assembly: ExportRenderer(typeof(StackLayout), typeof(ContentPageRenderer))]
namespace xamsta.UWP.Renderers
{
    public class ContentPageRenderer : LayoutRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
                UpdateBackgroundColor();
            }

        }

        protected override void UpdateBackgroundColor()
        {
            base.UpdateBackgroundColor();
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush"))
            {
                var brush = Windows.UI.Xaml.Application.Current.Resources["SystemControlAltHighAcrylicWindowBrush"] as AcrylicBrush;
                var tint = brush.TintColor;
                var opacity = brush.TintOpacity;
                var fallbackColor = brush.FallbackColor;
                var source = brush.BackgroundSource;
                this.Background = brush;
            }

        }
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }


    }
}
