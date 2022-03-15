using System;
using Xamarin.Forms;
using xamsta.UWP.Renderers;
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

            if (e.OldElement != null || Element != null)
            {
                TransitionCollection trs = new TransitionCollection();
                RepositionThemeTransition reposition = new RepositionThemeTransition();
                EntranceThemeTransition entrance = new EntranceThemeTransition();

                entrance.IsStaggeringEnabled = true;
                trs.Add(reposition);
                trs.Add(entrance);
                this.ChildrenTransitions = trs;
            }
        }
    }
}
