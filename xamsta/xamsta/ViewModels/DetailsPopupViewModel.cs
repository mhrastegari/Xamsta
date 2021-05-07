using xamsta.Views;
using xamsta.Models;
using Xamarin.Forms;
using xamsta.Helpers;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

namespace xamsta.ViewModels
{
    public class DetailsPopupViewModel : User
    {
        public ICommand OpenLinkCommand { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        public ICommand OpenProfileCommand { get; set; }

        public DetailsPopupViewModel()
        {
            Load();
            OpenLinkCommand = new Command(OpenLink);
            ClosePopupCommand = new Command(ClosePopup);
            OpenProfileCommand = new Command(OpenProfile);
        }

        async Task Load()
        {
            var userInfo = DetailsPopupView.user;
            IsVerified = userInfo.Value.IsVerified;
            UserName = userInfo.Value.Username.ToString();
            FullName = userInfo.Value.FullName.ToString();
            Biography = userInfo.Value.Biography.ToString();
            MediaCount = userInfo.Value.MediaCount.ToString();
            ExternalUrl = userInfo.Value.ExternalUrl.ToString();
            ProfilePicUrl = userInfo.Value.ProfilePicUrl.ToString();
            FollowerCount = InstagramService.UserCountFormat(userInfo.Value.FollowerCount);
            FollowingCount = InstagramService.UserCountFormat(userInfo.Value.FollowingCount);
        }

        async void OpenLink()
        {
            var website = ExternalUrl;
            await Browser.OpenAsync(website, BrowserLaunchMode.SystemPreferred);
        }

        async void ClosePopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync();
        }

        async void OpenProfile()
        {
            await InstagramService.OpenUser(UserName);
        }
    }
}
