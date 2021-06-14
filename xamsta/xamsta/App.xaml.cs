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
            var result = await InstagramService.LoadSession();

            if (result != null)
                MainPage = new NavigationPage(new HomeView());
            else
                MainPage = new NavigationPage(new MainView());
        }
    }
}
