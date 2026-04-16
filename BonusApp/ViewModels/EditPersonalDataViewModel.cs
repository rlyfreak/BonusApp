using BonusApp.Services;
using System.Globalization;

namespace BonusApp.ViewModels
{
    public class EditPersonalDataViewModel : BaseViewModel
    {
        private readonly ProfileService _profileService;

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        private string _middleName = string.Empty;
        public string MiddleName
        {
            get => _middleName;
            set => SetProperty(ref _middleName, value);
        }
        private string _birthDateText = string.Empty;
        public string BirthDateText
        {
            get => _birthDateText;
            set => SetProperty(ref _birthDateText, value);
        }
        public EditPersonalDataViewModel()
        {
            _profileService = ProfileService.Instance;
        }
        public void LoadData()
        {
            var profile = _profileService.GetProfile();
            LastName = profile.LastName;
            FirstName = profile.FirstName;
            MiddleName = profile.MiddleName;
            BirthDateText = profile.BirthDate.ToString("dd.MM.yyyy");
        }
        public bool Save()
        {
            if (
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(MiddleName) ||
            string.IsNullOrWhiteSpace(BirthDateText)
        )
            {
                return false;
            }

            bool parsed = DateTime.TryParseExact(
                BirthDateText.Trim(),
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime birthDate);

            if (!parsed)
            {
                return false;
            }

            DateTime today = DateTime.Today;
            if (birthDate > today)
            {
                return false;
            }

            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            if (age < 14 || age > 120)
            {
                return false;
            }

            _profileService.UpdatePersonalData(
                LastName.Trim(),
                FirstName.Trim(),
                MiddleName.Trim(),
                birthDate);

            return true;
        }
    }
}
