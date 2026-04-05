namespace BonusApp.Controls;

public partial class BottomNavBar : ContentView
{
    public static readonly BindableProperty CurrentTabProperty =
        BindableProperty.Create(
            nameof(CurrentTab),
            typeof(string),
            typeof(BottomNavBar),
            "Cards",
            propertyChanged: OnCurrentTabChanged);

    public string CurrentTab
    {
        get => (string)GetValue(CurrentTabProperty);
        set => SetValue(CurrentTabProperty, value);
    }

    public BottomNavBar()
    {
        InitializeComponent();
        ApplyState();
    }

    private static void OnCurrentTabChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BottomNavBar navBar)
        {
            navBar.ApplyState();
        }
    }

    private void ApplyState()
    {
        SetTabState(CardsIcon, CardsLabel, CurrentTab == "Cards");
        SetTabState(HistoryIcon, HistoryLabel, CurrentTab == "History");
        SetTabState(CatalogIcon, CatalogLabel, CurrentTab == "Catalog");
        SetTabState(ProfileIcon, ProfileLabel, CurrentTab == "Profile");
    }

    private void SetTabState(Image icon, Label label, bool isActive)
    {
        label.FontFamily = isActive ? "RubikMedium" : "RubikRegular";
        label.FontSize = isActive ? 16 : 14;
        label.TextColor = isActive
            ? Color.FromArgb("#F3F6F9")
            : Color.FromArgb("#CCF3F6F9");

        icon.Opacity = isActive ? 1.0 : 0.8;
    }

    private async void CardsTapped(object? sender, TappedEventArgs e)
    {
        if (CurrentTab == "Cards")
            return;

        await Shell.Current.GoToAsync("//CardsPage");
    }

    private async void HistoryTapped(object? sender, TappedEventArgs e)
    {
        if (CurrentTab == "History")
            return;

        await Shell.Current.GoToAsync("//HistoryPage");
    }

    private async void CatalogTapped(object? sender, TappedEventArgs e)
    {
        if (CurrentTab == "Catalog")
            return;

        await Shell.Current.GoToAsync("//CatalogPage");
    }

    private async void ProfileTapped(object? sender, TappedEventArgs e)
    {
        if (CurrentTab == "Profile")
            return;

        await Shell.Current.GoToAsync("//ProfilePage");
    }
}