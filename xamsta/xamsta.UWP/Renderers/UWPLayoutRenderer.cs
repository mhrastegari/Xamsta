using System;
using Xamarin.Forms;
using xamsta.UWP.Renderers;
using Microsoft.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Media.Animation;

[assembly: ExportRenderer(typeof(Layout), typeof(UWPLayoutRenderer))]
namespace xamsta.UWP.Renderers
{
    public class UWPLayoutRenderer : LayoutRenderer
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
                TransitionCollection trs = new TransitionCollection();
                RepositionThemeTransition enttrs = new RepositionThemeTransition();
                trs.Add(enttrs);
                this.ChildrenTransitions = trs;
            }

        }

        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }
    }
}
