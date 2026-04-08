using BonusApp.Models;

namespace BonusApp.Services
{
    public class ProfileService
    {
        private static readonly ProfileService _instance = new ProfileService();
        public static ProfileService Instance => _instance;

        private readonly UserProfile _profile = new()
        {
            LastName = "Иванов",
            FirstName = "Иван",
            MiddleName = "Иванович",
            BirthDate = new DateTime(1990, 1, 1),
            PhoneNumber = "+7 (999) 123-45-67",
            Email = "ivanov@example.com"
        };
        private ProfileService()
        {
        }
        public UserProfile GetProfile()
        {
            return _profile;
        }
    }
}
