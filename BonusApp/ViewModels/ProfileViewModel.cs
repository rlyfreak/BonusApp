using BonusApp.Services;

namespace BonusApp.ViewModels;

public class ProfileViewModel : BaseViewModel
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

        LastName = profile.LastName;
        FirstName = profile.FirstName;
        MiddleName = profile.MiddleName;
        BirthDateText = profile.BirthDate.ToString("dd.MM.yyyy");
        PhoneNumber = profile.PhoneNumber;
        Email = profile.Email;
    }
}