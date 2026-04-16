using BonusApp.Models;

namespace BonusApp.Services;

public class ProfileService
{
    private static readonly ProfileService _instance = new();
    public static ProfileService Instance => _instance;

    private readonly Dictionary<int, UserProfile> _profilesByUser = new();

    private ProfileService()
    {
    }

    public UserProfile GetProfile()
    {
        var profile = GetCurrentUserProfile();

        return new UserProfile
        {
            LastName = profile.LastName,
            FirstName = profile.FirstName,
            MiddleName = profile.MiddleName,
            BirthDate = profile.BirthDate,
            PhoneNumber = profile.PhoneNumber,
            Email = profile.Email
        };
    }

    public void UpdateEmail(string email)
    {
        GetCurrentUserProfile().Email = email;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        GetCurrentUserProfile().PhoneNumber = phoneNumber;
    }

    public void UpdatePersonalData(string lastName, string firstName, string middleName, DateTime birthDate)
    {
        var profile = GetCurrentUserProfile();
        profile.LastName = lastName;
        profile.FirstName = firstName;
        profile.MiddleName = middleName;
        profile.BirthDate = birthDate;
    }

    private UserProfile GetCurrentUserProfile()
    {
        int userId = SessionService.Instance.UserId;

        if (_profilesByUser.TryGetValue(userId, out var profile))
        {
            profile.PhoneNumber = SessionService.Instance.PhoneNumber;
            profile.Email = SessionService.Instance.Email;
            return profile;
        }

        profile = DemoAccountDefaults.IsCurrentSessionDemoAccount()
            ? DemoAccountDefaults.CreateProfile()
            : DemoAccountDefaults.CreateEmptyProfile();

        _profilesByUser[userId] = profile;
        return profile;
    }
}
