using AppKit;
using CoreGraphics;
using Foundation;
using Xamarin.Forms.Platform.MacOS;

namespace xamsta.macOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        NSWindow window;
        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled | NSWindowStyle.FullSizeContentView | NSWindowStyle.Miniaturizable;
            var rect = new CGRect(200, 1000, 1024, 768);
            window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            window.MinSize = new CGSize(535, 745);
            window.Title = "Xamsta";
            window.TitlebarAppearsTransparent = true;
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow
        {
            get { return window; }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            LoadApplication(new App());
            base.DidFinishLaunching(notification);
        }   
    }
}
