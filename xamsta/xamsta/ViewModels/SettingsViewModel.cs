using Xamarin.Forms;
using xamsta.Helpers;
using Xamarin.Essentials;
using System.Windows.Input;

namespace xamsta.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public bool LightTheme
        {
            get => Preferences.Get(nameof(LightTheme), false);
            set
            {
                Preferences.Set(nameof(LightTheme), value);
                OnPropertyChanged(nameof(LightTheme));
                SetAppTheme();
                SetMaterialFrameStyle();
            }
        }

        public bool DarkTheme
        {
            get => Preferences.Get(nameof(DarkTheme), false);
            set
            {
                Preferences.Set(nameof(DarkTheme), value);
                OnPropertyChanged(nameof(DarkTheme));
                SetAppTheme();
                SetMaterialFrameStyle();
            }
        }

        public bool DefaultTheme
        {
            get => Preferences.Get(nameof(DefaultTheme),true);
            set
            {
                Preferences.Set(nameof(DefaultTheme), value);
                OnPropertyChanged(nameof(DefaultTheme));
                SetAppTheme();
                SetMaterialFrameStyle();
            }
        }

        public bool IsAcrylic
        {
            get => Preferences.Get(nameof(IsAcrylic), Device.RuntimePlatform == Device.Android ? false : true);
            set
            {
                Preferences.Set(nameof(IsAcrylic), value);
                OnPropertyChanged(nameof(IsAcrylic));
                SetMaterialFrameStyle();
            }
        }

        public ICommand LogoutCommand { get; set; }
        public ICommand ViewAccountCommand { get; set; }
        public ICommand DoneCommand { get; set; }

        public SettingsViewModel()
        {
            LogoutCommand = new Command(Logout);
            ViewAccountCommand = new Command(ViewAccount);
            DoneCommand = new Command(Done);
            SetAppTheme();
        }

        public void SetAppTheme()
        {
            if (DefaultTheme == true)
                Application.Current.UserAppTheme = OSAppTheme.Unspecified;
            else if (DarkTheme == true)
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            else if (LightTheme == true)
                Application.Current.UserAppTheme = OSAppTheme.Light;
            else
                Application.Current.UserAppTheme = OSAppTheme.Unspecified;
        }

        public void SetMaterialFrameStyle()
        {
            var mf = new MaterialFrame();

            if (IsAcrylic == true)
            {
                App.Current.Resources["BlurTheme"] = MaterialFrame.Theme.AcrylicBlur;
            }
            else if(IsAcrylic == false && mf.MaterialBlurStyle == MaterialFrame.BlurStyle.Dark)
            {
                App.Current.Resources["BlurTheme"] = MaterialFrame.Theme.Dark;
            }
            else if (IsAcrylic == false && mf.MaterialBlurStyle == MaterialFrame.BlurStyle.ExtraLight)
            {
                App.Current.Resources["BlurTheme"] = MaterialFrame.Theme.Acrylic;
            }

        }

        async void Logout()
        {
            await InstagramService.Logout();
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        async void ViewAccount()
        {
            var currentUser = InstagramService.GetLoggedUser();
            await InstagramService.OpenUser(currentUser.UserName);
        }

        async void Done()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
