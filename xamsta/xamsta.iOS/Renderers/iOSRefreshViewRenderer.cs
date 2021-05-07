using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using xamsta.iOS.Renderers;

[assembly: ExportRenderer(typeof(RefreshView), typeof(iOSRefreshViewRenderer))]
namespace xamsta.iOS.Renderers
{
    public class iOSRefreshViewRenderer : RefreshViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<RefreshView> e)
        {
            base.OnElementChanged(e);

            foreach (var nativeView in Subviews)
                updateRefreshSettings(nativeView);

        }

        void updateRefreshSettings(UIView view)
        {
            if (view is UIScrollView)
            {
                var scrollView = view as UIScrollView;
                if (scrollView.RefreshControl != null)
                {
                    var bounds = scrollView.RefreshControl.Bounds;
                    scrollView.RefreshControl.Bounds = new CGRect(bounds.X, -(150), bounds.Width, bounds.Height);
                }
            }
        }
    }
}