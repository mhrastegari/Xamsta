using System.Windows.Input;
using Xamarin.Forms;
using xamsta.Views;
using System.Threading.Tasks;
using xamsta.Helpers;

namespace xamsta.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string username { get; set; }
        public string password { get; set; }

        private bool _buttonVisibility = true;
        public bool ButtonVisibility
        {
            get => _buttonVisibility;
            set
            {
                _buttonVisibility = value;
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }

        public ICommand LoginCommand { get; set; }

        public MainViewModel()
        {
            LoadSession();
            LoginCommand = new Command(Login);
        }

        public async Task LoadSession()
        {
            var result = await InstagramService.LoadSession();
            if (result != null)
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()), false);
        }

        public async void Login()
        {
            ButtonVisibility = false;
            var login = new InstagramService.LoginData() { UserName = username, Password = password };
            bool result = await InstagramService.Login(login.UserName,login.Password);
            if (result == true)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()), false);
            }
            else if (result == false)
            {
                ButtonVisibility = true;
            }
        }
    }
}
