using System.Windows.Input;
using System.Collections.ObjectModel;
using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.ViewModels
{
    public class CatalogViewModel : BaseViewModel
    {
        private readonly CafeService _cafeService;
        private readonly CardService _cardService;

        private List<CatalogCardItem> _allItems = new();
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    ApplyFilter();
                }
            }
        }
        private bool _hasItems;
        public bool HasItems
        {
            get => _hasItems;
            set => SetProperty(ref _hasItems, value);
        }
        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value);
        }
        public ObservableCollection<CatalogCardItem> Items { get; } = new();
        public ICommand AddCardCommand { get; }
        public CatalogViewModel()
        {
            _cafeService = CafeService.Instance;
            _cardService = CardService.Instance;

            AddCardCommand = new Command<CatalogCardItem>(item =>
            {
                if (item == null)
                    return;

                var created = _cardService.AddCard(item.Name);

                if (created != null)
                {
                    LoadCatalog();
                }
            });
        }
        public void LoadCatalog()
        {
            _allItems = _cafeService.GetCafes()
                .Where(cafe => !_cardService.HasCardForCafe(cafe.Name))
                .Select(cafe => new CatalogCardItem
                {
                    Id = cafe.ID,
                    Name = cafe.Name
                })
                .ToList();
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            IEnumerable<CatalogCardItem> filtered = _allItems;
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string query = SearchText.Trim().ToLower();
                filtered = filtered.Where(x => x.Name.ToLower().Contains(query));
            }
            Items.Clear();
            foreach (var item in filtered)
            {
                Items.Add(item);
            }
            HasItems = Items.Count > 0;
            IsEmpty = Items.Count == 0;
        }
    }
}
