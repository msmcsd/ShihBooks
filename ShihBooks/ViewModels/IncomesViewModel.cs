using CommunityToolkit.Mvvm.ComponentModel;
using ShihBooks.Core.Incomes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ShihBooks.UseCases.Interfaces.Incomes;
using ShihBooks.Views.Incomes;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.UseCases.Interfaces.IncomeSources;

namespace ShihBooks.ViewModels
{
    [QueryProperty("Year", "year")]
    [QueryProperty("Month", "month")]
    public partial class IncomesViewModel : BaseViewModel
    {
        private readonly IViewIncomesByMonthUseCase _viewIncomesByMonthUseCase;
        private readonly IDeleteIncomeUseCase _deleteIncomeUseCase;

        private List<IncomeDetails> _cachedIncomes { get; set; } = new();

        [ObservableProperty]
        private int _year;
        
        [ObservableProperty]
        private int _month;

        public ObservableCollection<IncomeDetails> FilteredIncomes { get; set; } = new();

        public IncomesViewModel(IViewIncomesByMonthUseCase viewIncomesyMonthUseCase,
                                IDeleteIncomeUseCase deleteIncomeUseCase)
        {
            _viewIncomesByMonthUseCase = viewIncomesyMonthUseCase;
            _deleteIncomeUseCase = deleteIncomeUseCase;
        }

        public override async Task GetEntitiesAsync()
        {
            if (IsBusy) return;

            if (FilteredIncomes.Count > 0)
            {
                FilteredIncomes.Clear();
            }

            try
            {
                IsBusy = true;

                _cachedIncomes = await _viewIncomesByMonthUseCase.ExecuteAsync(Year, Month);

                if (_cachedIncomes?.Count > 0)
                {
                    foreach (var e in _cachedIncomes)
                    {
                        // There is not AddRange currently. This will notify the
                        // collection change every time an element is added.
                        FilteredIncomes.Add(e);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GoToDetailsAsync(IncomeDetails income)
        {
            if (income is null) return;

            await Shell.Current.GoToAsync($"{nameof(IncomeDetailsPage)}", true,
                new Dictionary<string, object>() {
                    { "Income", income }
                });

            SearchText = "";
        }

        [RelayCommand]
        public async Task DeleteIncomeAsync(IncomeDetails income)
        {
            if (income != null)
            {
                var ret = await _deleteIncomeUseCase.ExecuteAsync(income.Id);
                if (ret > 0)
                {
                    _cachedIncomes.Remove(income);
                    FilteredIncomes.Remove(income);
                }

                SearchText = "";
            }
        }

        public override async Task SearchEntityAsync()
        {
            var list = SearchText?.Length <= 0 ?
                            _cachedIncomes :
                            _cachedIncomes.Where(t => t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            if (FilteredIncomes.Count > 0) FilteredIncomes.Clear();

            foreach (var t in list)
            {
                FilteredIncomes.Add(t);
            }
        }

        public override async Task AddEntityAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(IncomeDetailsPage)}", true,
                new Dictionary<string, object>()
                {
                    { "Income", null }
                });

            SearchText = "";
        }
    }
}
