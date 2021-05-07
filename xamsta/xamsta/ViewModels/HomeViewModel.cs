using InstagramApiSharp.Classes.Models;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using xamsta.Helpers;
using xamsta.Views;

namespace xamsta.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private IEnumerable<InstaUserShort> _unFollowersList;
        public IEnumerable<InstaUserShort> unFollowersList
        {
            get => _unFollowersList;
            set
            {
                if (_unFollowersList != value)
                {
                    _unFollowersList = value;
                    OnPropertyChanged(nameof(unFollowersList));
                }
            }
        }

        private string _unfollowersCount;
        public string unfollowersCount
        {
            get => _unfollowersCount;
            set
            {
                if (_unfollowersCount != value)
                {
                    _unfollowersCount = value;
                    OnPropertyChanged(nameof(unfollowersCount));
                }
            }
        }

        private string? _userName;
        public string? UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private string? _fullName;
        public string? FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        private string _profilePicUrl = "ProfileDefault.png";
        public string? ProfilePicUrl
        {
            get => _profilePicUrl;
            set
            {
                if (_profilePicUrl != value)
                {
                    _profilePicUrl = value;
                    OnPropertyChanged(nameof(ProfilePicUrl));
                }
            }
        }

        public ICommand UnfollowCommand { get; set; }
        public ICommand SelectedUserCommand { get; set; }
        public ICommand SettingsViewCommand { get; set; }

        public HomeViewModel()
        {
            SettingsViewCommand = new Command(SettingsView);
            UnfollowCommand = new Command<InstaUserShort>(Unfollow);
            SelectedUserCommand = new Command<InstaUserShort>(SelectedUser);
            load();
        }

        async Task load()
        {
            var connection = Connectivity.NetworkAccess;
            var unfollowers = await InstagramService.GetUnfollowers();
            var currentUser = InstagramService.InstaApi.GetLoggedUser();
            var userInfo = await InstagramService.InstaApi.UserProcessor.GetUserInfoByUsernameAsync(currentUser.LoggedInUser.UserName);

            if (userInfo.Value != null && connection == NetworkAccess.Internet)
            {
                UserName = userInfo.Value.Username;
                ProfilePicUrl = userInfo.Value.ProfilePicUrl;
                FullName = userInfo.Value.FullName;
                unFollowersList = unfollowers;
                unfollowersCount = unfollowers.Count().ToString();
            }

            else if (userInfo.Value == null && connection == NetworkAccess.Internet)
            {
                bool res = await Application.Current.MainPage.DisplayAlert("Something went wrong!", "try to reopen Xamsta or ....", "Logout", "Refresh App");
                
                switch (res)
                {
                    case true:
                        await InstagramService.Logout();
                        break;
                    case false:
                        await Reload();
                        break;
                }
            }

            else if (connection != NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlert("You're Offline!", "Please connect to an internet connection and reopen the app!", "Ok");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        async Task Reload()
        {
            var unfollowers = await InstagramService.GetUnfollowers();
            var currentUser = InstagramService.InstaApi.GetLoggedUser();
            var userInfo = await InstagramService.InstaApi.UserProcessor.GetUserInfoByUsernameAsync(currentUser.LoggedInUser.UserName);
            UserName = userInfo.Value.Username;
            FullName = userInfo.Value.FullName;
            ProfilePicUrl = userInfo.Value.ProfilePicUrl;
            unFollowersList = unfollowers;
            unfollowersCount = unfollowers.Count().ToString();
        }

        async void SettingsView()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new SettingsView());
        }

        async void SelectedUser(InstaUserShort user)
        {
            var Info = await InstagramService.InstaApi.UserProcessor.GetUserInfoByUsernameAsync(user.UserName);
            await App.Current.MainPage.Navigation.PushPopupAsync(new DetailsPopupView(Info));
        }

        public async void Unfollow(InstaUserShort userinfo)
        {
            try
            {
                if(userinfo.IsPrivate==true)
                {
                    var res = await Application.Current.MainPage.DisplayActionSheet($"If you change your mind, you'll have to request to follow {userinfo.UserName} again.", "Cancel", "Unfollow");
                    switch (res)
                    {
                        case "Unfollow":
                            await InstagramService.UnFollow(userinfo.Pk);
                            break;
                    }
                }

                else if (userinfo.IsPrivate == false)
                {
                    await InstagramService.UnFollow(userinfo.Pk);
                }
            }

            finally
            {
                await Reload();
            }
        }
    }
}
