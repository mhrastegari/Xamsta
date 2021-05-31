using System;
using AppKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(RadioButton), typeof(xamsta.macOS.Renderers.macOSRadioButtonRenderer))]
namespace xamsta.macOS.Renderers
{
	public class macOSRadioButtonRenderer : RadioButtonRenderer
	{
		void HandleActivated(object sender, EventArgs args)
		{
			if (Element == null || sender == null)
				return;
			
			Element.IsChecked = (sender as NSButton).State == NSCellStateValue.On;
		}

		protected override void Dispose(bool disposing)
		{
			if (Control != null)
				Control.Activated -= HandleActivated;

			base.Dispose(disposing);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<RadioButton> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.Activated += HandleActivated;

				if (e.NewElement.IsChecked)
					Control.State = NSCellStateValue.On;
			}
		}
	}
}
