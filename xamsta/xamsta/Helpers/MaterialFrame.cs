using Xamarin.Forms;

namespace xamsta.Helpers
{
    public class MaterialFrame : Sharpnado.MaterialFrame.MaterialFrame
    {
        public static readonly BindableProperty macOSBehindWindowBlurProperty = BindableProperty.Create(
            nameof(macOSBehindWindowBlur),
            typeof(bool),
            typeof(MaterialFrame),
            defaultValueCreator: _ => false);

        public static readonly BindableProperty UwpHostBackdropBlurProperty = BindableProperty.Create(
            nameof(UwpHostBackdropBlur),
            typeof(bool),
            typeof(MaterialFrame),
            defaultValueCreator: _ => false);

        /// <summary>
        /// macOS only.
        /// BehindWindow reveals the desktop wallpaper and other windows that are behind the currently active app.
        /// If not set, the default in app WithinWindow take over.
        /// </summary>
        public bool macOSBehindWindowBlur
        {
            get => (bool)GetValue(macOSBehindWindowBlurProperty);
            set => SetValue(macOSBehindWindowBlurProperty, value);
        }

        /// <summary>
        /// UWP only.
        /// HostBackdrop reveals the desktop wallpaper and other windows that are behind the currently active app.
        /// If not set, the default in app Backdrop take over.
        /// </summary>
        public bool UwpHostBackdropBlur
        {
            get => (bool)GetValue(UwpHostBackdropBlurProperty);
            set => SetValue(UwpHostBackdropBlurProperty, value);
        }
    }
}
