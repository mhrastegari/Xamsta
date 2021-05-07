namespace xamsta.Views
{
    public partial class DetailsPopupView
    {
        public static InstagramApiSharp.Classes.IResult<InstagramApiSharp.Classes.Models.InstaUserInfo> user;
        public DetailsPopupView(InstagramApiSharp.Classes.IResult<InstagramApiSharp.Classes.Models.InstaUserInfo> info)
        {
            user = info;
            InitializeComponent();
        }
    }
}