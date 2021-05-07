using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using xamsta.UWP.Renderers;
using Windows.UI.Xaml;

//[assembly:ExportRenderer(typeof(ActivityIndicator),typeof(UWPActivityIndicatorRenderer))]
namespace xamsta.UWP.Renderers
{
    public class UWPActivityIndicatorRenderer : ViewRenderer<ActivityIndicator, FrameworkElement>
    {
        //protected override void OnElementChanged(ElementChangedEventArgs<ActivityIndicator> e)
        //{
        //    base.OnElementChanged(e);
        //    if (Control != null) { return; }
        //    var progressRing = new ProgressRing
        //    {
        //        IsActive = true,
        //        Visibility = Visibility.Visible,
        //        IsEnabled = true,
        //        Width = 25
        //    };
        //    SetNativeControl(progressRing);
        //}

    }
}
