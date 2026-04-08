using BonusApp.Services;

namespace BonusApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly ProfileService _profileService;
        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }
        private string _birthDateText = string.Empty;
        public string BirthDateText
        {
            get => _birthDateText;
            set => SetProperty(ref _birthDateText, value);
        }
        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }   
        public ProfileViewModel()
        {
            _profileService = ProfileService.Instance;
        }
        public void LoadProfile()
        {
            var profile = _profileService.GetProfile();
            FullName = $"{profile.LastName} {profile.FirstName} {profile.MiddleName}".Trim();
            BirthDateText = profile.BirthDate.ToString("dd.MMMM.yyyy");
            PhoneNumber = profile.PhoneNumber;
            Email = profile.Email;
        }
    }
}
