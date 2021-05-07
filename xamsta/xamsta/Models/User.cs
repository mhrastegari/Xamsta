namespace xamsta.Models
{
    public class User : ViewModels.BaseViewModel
    {
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

        public bool? IsVerified { get; set; }
        public bool? IsVisible { get; set; }
        public string? FollowerCount { get; set; }
        public string? FollowingCount { get; set; }
        public string? Biography { get; set; }
        public string? ExternalUrl { get; set; }
        public string? MediaCount { get; set; }
    }
}
