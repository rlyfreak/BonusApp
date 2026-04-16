using BonusApp.Services;
using System.Text.RegularExpressions;

namespace BonusApp.ViewModels;

public class EditPhoneViewModel : BaseViewModel
{
    private readonly ProfileService _profileService;

    private string _phoneNumber = string.Empty;
    public string PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value);
    }

    public EditPhoneViewModel()
    {
        _profileService = ProfileService.Instance;
    }

    public void LoadPhone()
    {
        PhoneNumber = _profileService.GetProfile().PhoneNumber;
    }

    public bool SavePhone()
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber))
            return false;

        string normalizedPhone = Regex.Replace(PhoneNumber.Trim(), @"[\s\-\(\)]", string.Empty);

        if (!Regex.IsMatch(normalizedPhone, @"^\+?\d{10,15}$"))
            return false;

        _profileService.UpdatePhoneNumber(normalizedPhone);
        return true;
    }
}
