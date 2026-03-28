using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.Views;

public partial class AddCardPage : ContentPage
{
    private readonly CafeService _cafeService;
    private readonly CardService _cardService;

    public AddCardPage()
    {
        InitializeComponent();

        _cafeService = CafeService.Instance;
        _cardService = CardService.Instance;

        CafesCollectionView.ItemsSource = _cafeService.GetCafes();
    }

    private async void CafesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Cafe selectedCafe)
            return;

        CafesCollectionView.SelectedItem = null;

        if (_cardService.HasCardForCafe(selectedCafe.Name))
        {
            await DisplayAlert(
                "Карта уже существует",
                $"У вас уже есть карта заведения {selectedCafe.Name}.",
                "OK");
            return;
        }

        bool confirm = await DisplayAlert(
            "Создание карты",
            $"Оформить карту лояльности для заведения {selectedCafe.Name}?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        var createdCard = _cardService.AddCard(selectedCafe.Name);

        if (createdCard == null)
        {
            await DisplayAlert("Ошибка", "Не удалось создать карту.", "OK");
            return;
        }

        await DisplayAlert(
            "Готово",
            $"Карта для заведения {createdCard.CafeName} успешно создана.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }
}