using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class CatalogPage : ContentPage
{
    private readonly CatalogViewModel _viewModel;

    public CatalogPage()
    {
        InitializeComponent();
        _viewModel = new CatalogViewModel();
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCatalog();
    }
}