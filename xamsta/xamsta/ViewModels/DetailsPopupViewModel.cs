using System;
using xamsta.Views;
using xamsta.Models;
using Xamarin.Forms;
using xamsta.Helpers;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using Rg.Plugins.Popup.Extensions;
using InstagramApiSharp.Classes.Models;

namespace xamsta.ViewModels
{
    public class DetailsPopupViewModel : User
    {
        public IResult<InstaUserInfo> userInfo { get; set; }

        public ICommand OpenLinkCommand { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        public ICommand OpenProfileCommand { get; set; }
        public ICommand ShareImageCommand { get; set; }

        public DetailsPopupViewModel()
        {
            Load();
            OpenLinkCommand = new Command(OpenLink);
            ClosePopupCommand = new Command(ClosePopup);
            OpenProfileCommand = new Command(OpenProfile);
            ShareImageCommand = new Command(ShareImage);
        }

        void Load()
        {
            userInfo = DetailsPopupView.user;
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

        async void ShareImage()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = $"Sharing {UserName}'s Profile Image...",
                Uri = userInfo.Value.ProfilePicUrl.ToString()
            });
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
