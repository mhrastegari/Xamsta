using Xamarin.Forms;
using xamsta.Views;
using xamsta.ViewModels;
using xamsta.Helpers;
using System;

namespace xamsta
{
    public partial class App : Application
    {
        public static INavigation Navigation { get; internal set; }
        public App()
        {
            InitializeComponent();

            SettingsViewModel settingsViewModel = new SettingsViewModel();
            settingsViewModel.SetAppTheme();
            settingsViewModel.SetMaterialFrameStyle();
            Sharpnado.MaterialFrame.Initializer.Initialize(loggerEnable: false,false);

            Load();
        }

        private async void Load()
        {
            var result = await InstagramService.LoadSession();
            if (result != null)
                MainPage = new NavigationPage(new HomeView());
            else
                MainPage = new NavigationPage(new MainView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
