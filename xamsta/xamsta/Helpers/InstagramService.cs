using System;
using System.Linq;
using xamsta.Views;
using Xamarin.Forms;
using InstagramApiSharp;
using Xamarin.Essentials;
using InstagramApiSharp.API;
using System.Threading.Tasks;
using InstagramApiSharp.Logger;
using InstagramApiSharp.Classes;
using System.Collections.Generic;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes.Models;

namespace xamsta.Helpers
{
    public static class InstagramService 
    {
        public static string json;
        public static InstaUserInfo info;
        public static IInstaApi InstaApi;
        public const string StateFileName = "state.json";
        private static IResult<InstaUserShortList> followersList;
        private static IResult<InstaUserShortList> followingsList;
        private static IEnumerable<InstaUserShort> unfollowerList;

        public static string GetStateData()
        {
            return InstaApi.GetStateDataAsString();
        }

        public static async Task UnFollow(long PK)
        {
            await InstaApi.UserProcessor.UnFollowUserAsync(PK);
        }

        public static async Task<IResult<InstaUserInfo>> GetUserInfoByUsernameAsync(string UserName)
        {
            var userInfo = await InstaApi.UserProcessor.GetUserInfoByUsernameAsync(UserName); 

            if(userInfo.Value==null)
                return null;
            else
                return userInfo;
        }

        public static async Task<IResult<InstaMediaList>> GetUserMediaAsync(string userName, PaginationParameters paginationParameters)
        {
            var PostsList = await InstaApi.UserProcessor.GetUserMediaAsync(userName, paginationParameters);
            return PostsList;
        }

        public static UserSessionData GetLoggedUser()
        {
            try
            {
                UserSessionData currentUser = InstaApi.GetLoggedUser();
                return currentUser;
            }
            catch
            {
                return null;
            }
        }

        public static async Task Logout()
        {
            var currentUser = GetLoggedUser();
            var result = await Application.Current.MainPage.DisplayAlert($"Log out of {currentUser.UserName}?", null, "Logout", "Cancel");
            switch (result)
            {
                case true:
                    SecureStorage.RemoveAll();
                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new MainView()), false);
                    break;
            }
        }

        public static string UserCountFormat(long num)
        {
            if (num >= 100000000)
                return (num / 1000000D).ToString("0.#M");
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##M");
            if (num >= 100000)
                return (num / 1000D).ToString("0.#k");
            if (num >= 10000)
                return (num / 1000D).ToString("0.##k");

            return num.ToString("#,0");
        }

        [Obsolete]
        public static async Task OpenUser(string username)
        {
            var shortInfo = new InstaUserShort();
            shortInfo.UserName = username;
            var browseruri = $"https://instagram.com/{username}";
            var appuri = await Launcher.CanOpenAsync($"instagram://");

            if (appuri)
                Device.OpenUri(new Uri($"instagram://user?username={username}"));
            else
                await Browser.OpenAsync(browseruri, BrowserLaunchMode.SystemPreferred);
        }

        public static async Task<bool> Login(string username, string password)
        {
            var login = new LoginData() { UserName = username, Password = password };

            try
            {
                var userSession = new UserSessionData
                {
                    UserName = login.UserName,
                    Password = login.Password
                };

                InstaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.All))
                .Build();

                var loginResult = await InstaApi.LoginAsync();

                if (loginResult.Succeeded)
                {
                    await SaveSession();
                    return true;
                }

                else
                {
                    switch (loginResult.Value)
                    {
                        case InstaLoginResult.InactiveUser:
                            await Application.Current.MainPage.DisplayAlert("Error", "This user is not active.", "Ok");
                            break;

                        case InstaLoginResult.InvalidUser:
                            await Application.Current.MainPage.DisplayAlert("Error", "Username is invalid.", "Ok");
                            break;

                        case InstaLoginResult.BadPassword:
                            await Application.Current.MainPage.DisplayAlert("Error", "Password not mach", "Ok");
                            break;

                        case InstaLoginResult.Exception:
                            await Application.Current.MainPage.DisplayAlert("Error", "Exception throws:\n" + loginResult.Info?.Message, "Ok");
                            break;

                        case InstaLoginResult.LimitError:
                            await Application.Current.MainPage.DisplayAlert("Error", "Limit error (you should wait 10 minutes).", "Ok");
                            break;

                        case InstaLoginResult.TwoFactorRequired:
                            var result = await Acr.UserDialogs.UserDialogs.Instance.PromptAsync("Enter the 6-digits code that instagram has been sent to your number \n", "Enter Security Code", "Auth", "Cancel");
                            if(result.Ok)
                            { 
                                if (InstaApi == null)
                                    return false;
                                if (string.IsNullOrEmpty(result.Text))
                                {
                                    await Application.Current.MainPage.DisplayAlert("Error", "Please type your two factor code and then press Auth button.","Ok");
                                    return false;
                                }
                                var twoFactorLogin = await InstaApi.TwoFactorLoginAsync(result.Text);
                                if (twoFactorLogin.Succeeded)
                                {
                                    // connected
                                    // save session
                                    await SaveSession();
                                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()), false);
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert("Error", twoFactorLogin.Info.Message, "Ok");
                                }
                            }
                            break;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }
        }

        public static async Task<IEnumerable<InstaUserShort>> GetUnfollowers()
        {
            var currentUser = InstaApi.GetLoggedUser();
            followersList = await InstaApi.UserProcessor.GetUserFollowersAsync(currentUser.LoggedInUser.UserName, PaginationParameters.MaxPagesToLoad(6));
            followingsList = await InstaApi.UserProcessor.GetUserFollowingAsync(currentUser.LoggedInUser.UserName, PaginationParameters.MaxPagesToLoad(6));
            HashSet<string> followerUsernameList = new HashSet<string>(followersList.Value.Select(s => s.UserName));
            unfollowerList = followingsList.Value.Where(m => !followerUsernameList.Contains(m.UserName));
            return unfollowerList;
        }

        public static async Task SaveSession()
        {
            if (InstaApi == null)
                return;
            if (!InstaApi.IsUserAuthenticated)
                return;

            try
            {
                json = InstaApi.GetStateDataAsString();
                await SecureStorage.SetAsync(StateFileName, json);
            }
            catch { }
        }

        public static async Task<LoginData?> LoadSession()
        {
            LoginData loginData = new LoginData();

            try
            {
                var file = await SecureStorage.GetAsync(StateFileName);

                if (string.IsNullOrEmpty(file))
                    return null;

                InstaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(UserSessionData.Empty)
                    .UseLogger(new DebugLogger(LogLevel.All))
                    .Build();
                InstaApi.LoadStateDataFromString(file);
                if (!InstaApi.IsUserAuthenticated)
                {
                    InstaApi = null;
                    return null;
                }
                loginData.UserName = InstaApi.GetLoggedUser().UserName;
                loginData.Password = InstaApi.GetLoggedUser().Password;

               
                return loginData;
            }
            catch { InstaApi = null; return null; }
        }

        public struct LoginData
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
