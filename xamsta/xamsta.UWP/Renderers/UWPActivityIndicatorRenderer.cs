using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using xamsta.UWP.Renderers;
using Windows.UI.Xaml;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(UWPActivityIndicatorRenderer))]
namespace xamsta.UWP.Renderers
{
	public class UWPActivityIndicatorRenderer : ViewRenderer<ActivityIndicator, ProgressRing>
	{
		object _foregroundDefault;

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
						Width = 25
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
			_foregroundDefault = Control.Foreground;
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
