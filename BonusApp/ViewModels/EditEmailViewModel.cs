using BonusApp.Services;

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

        _profileService.UpdateEmail(Email.Trim());
        return true;
    }
}