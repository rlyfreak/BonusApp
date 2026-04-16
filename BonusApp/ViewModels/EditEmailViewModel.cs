using BonusApp.Services;
using System.Net.Mail;

namespace BonusApp.ViewModels;

public class EditEmailViewModel : BaseViewModel
{
    private readonly ProfileService _profileService;

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public EditEmailViewModel()
    {
        _profileService = ProfileService.Instance;
    }

    public void LoadEmail()
    {
        Email = _profileService.GetProfile().Email;
    }

    public bool SaveEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
            return false;

        string normalizedEmail = Email.Trim();

        try
        {
            var mailAddress = new MailAddress(normalizedEmail);
            if (!string.Equals(mailAddress.Address, normalizedEmail, StringComparison.OrdinalIgnoreCase))
                return false;
        }
        catch
        {
            return false;
        }

        _profileService.UpdateEmail(normalizedEmail);
        return true;
    }
}
