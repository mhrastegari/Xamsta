namespace xamsta.iOS
{
    [Foundation.Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIKit.UIApplication app, Foundation.NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            OliveTree.Transitions.TransitionsLibrary.Register<OliveTree.Transitions.iOS.Provider>();
            Sharpnado.MaterialFrame.iOS.iOSMaterialFrameRenderer.Init();
            Xamarin.Forms.Nuke.FormsHandler.Init();
            Rg.Plugins.Popup.Popup.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
