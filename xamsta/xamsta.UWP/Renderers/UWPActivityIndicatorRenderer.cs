using Xamarin.Forms;
using Windows.UI.Xaml;
using xamsta.UWP.Renderers;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(UWPActivityIndicatorRenderer))]
namespace xamsta.UWP.Renderers
{
	public class UWPActivityIndicatorRenderer : ViewRenderer<ActivityIndicator, ProgressRing>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<ActivityIndicator> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					SetNativeControl(new ProgressRing
					{
						IsActive = true,
						Visibility = Visibility.Visible,
						IsEnabled = true,
					});

					Control.Loaded += OnControlLoaded;
				}

				UpdateIsRunning();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == ActivityIndicator.IsRunningProperty.PropertyName || e.PropertyName == VisualElement.OpacityProperty.PropertyName)
				UpdateIsRunning();
			else if (e.PropertyName == ActivityIndicator.ColorProperty.PropertyName)
				UpdateColor();
		}

		void OnControlLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			UpdateColor();
		}

		void UpdateColor()
		{
			Color color = Element.Color;
			Control.Foreground = color.ToBrush();
		}

		void UpdateIsRunning()
		{
			Control.Opacity = Element.IsRunning ? Element.Opacity : 0;
		}
	}
}
