using BonusApp.Models;

namespace BonusApp.Services;

public class ProfileService
{
    private static readonly ProfileService _instance = new ProfileService();
    public static ProfileService Instance => _instance;

    private readonly UserProfile _profile = new()
    {
        LastName = "Бондаренко",
        FirstName = "Дмитрий",
        MiddleName = "Сергеевич",
        BirthDate = new DateTime(2007, 9, 10),
        PhoneNumber = "+79991234567",
        Email = "rlyfreak000@gmail.com"
    };

    private ProfileService()
    {
    }

    public UserProfile GetProfile()
    {
        return _profile;
    }
    public void UpdateEmail(string email)
    {
        _profile.Email = email;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        _profile.PhoneNumber = phoneNumber;
    }

    public void UpdatePersonalData(string lastName, string firstName, string middleName, DateTime birthDate)
    {
        _profile.LastName = lastName;
        _profile.FirstName = firstName;
        _profile.MiddleName = middleName;
        _profile.BirthDate = birthDate;
    }
}