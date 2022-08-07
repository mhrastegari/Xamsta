using System;
using APES.UI.XF;
using xamsta.Views;
using Xamarin.Forms;
using xamsta.Helpers;
using xamsta.ViewModels;

namespace xamsta
{
    public partial class App : Application
    {
        public static INavigation Navigation { get; internal set; }

        public App()
        {
            InitializeComponent();

            Sharpnado.MaterialFrame.Initializer.Initialize(loggerEnable: false, false);

            SettingsViewModel settingsViewModel = new SettingsViewModel();
            settingsViewModel.SetMaterialFrameStyle();
            settingsViewModel.SetAppTheme();

            ContextMenuContainer.Init();

            Load();
        }

        private async void Load()
        {
            InstagramService.LoginData? result = await InstagramService.LoadSession();

            MainPage = result != null ? new NavigationPage(new HomeView()) : new NavigationPage(new MainView());
            //MainPage =  new NavigationPage(new HomeView());
        }
    }
}
