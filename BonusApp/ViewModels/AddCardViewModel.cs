using System.Collections.ObjectModel;
using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.ViewModels;

public class AddCardViewModel : BaseViewModel
{
    private readonly CafeService _cafeService;
    private readonly CardService _cardService;

    private Cafe? _selectedCafe;
    public Cafe? SelectedCafe
    {
        get => _selectedCafe;
        set => SetProperty(ref _selectedCafe, value);
    }

    public ObservableCollection<Cafe> Cafes { get; } = new();

    public AddCardViewModel()
    {
        _cafeService = CafeService.Instance;
        _cardService = CardService.Instance;
    }

    public void LoadCafes()
    {
        Cafes.Clear();

        var cafes = _cafeService.GetCafes();

        foreach (var cafe in cafes)
        {
            Cafes.Add(cafe);
        }
    }

    public bool HasCardForSelectedCafe()
    {
        if (SelectedCafe == null)
            return false;

        return _cardService.HasCardForCafe(SelectedCafe.Name);
    }

    public bool CreateCardForSelectedCafe()
    {
        if (SelectedCafe == null)
            return false;

        var createdCard = _cardService.AddCard(SelectedCafe.Name);
        return createdCard != null;
    }

    public void ClearSelection()
    {
        SelectedCafe = null;
    }
}