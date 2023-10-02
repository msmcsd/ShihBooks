using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.Expenses;
using ShihBooks.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShihBooks.ViewModels
{
    [QueryProperty("Year", "year")]
    [QueryProperty("Month", "month")]
    public partial class ExpensesViewModel : BaseViewModel
    {
        private readonly IViewExpensesByMonthUseCase _viewExpensesByMonthUseCase;
        private readonly IDeleteExpenseUseCase _deleteExpenseUseCase;

        private List<ExpenseView> _cachedExpenses { get; set; } = new();

        [ObservableProperty]
        private int _year;

        [ObservableProperty]
        private int _month;

        [ObservableProperty]
        private string _searchText;

        public ObservableCollection<ExpenseView> FilteredExpenses { get; set; } = new();

        public ExpensesViewModel(IViewExpensesByMonthUseCase viewExpensesByMonthUseCase,
                                 IDeleteExpenseUseCase deleteExpenseUseCase)
        {
            _viewExpensesByMonthUseCase = viewExpensesByMonthUseCase;
            _deleteExpenseUseCase = deleteExpenseUseCase;
        }

        public async Task LoadExpensesByMonthAsync(int year, int month)
        {
            if (IsBusy) return;

            if (FilteredExpenses.Count > 0)
            {
                FilteredExpenses.Clear();
            }

            try
            {
                IsBusy = true;

                _cachedExpenses = await _viewExpensesByMonthUseCase.ExecuteAsync(year, month);

                if (_cachedExpenses?.Count > 0)
                {
                    foreach (var e in _cachedExpenses)
                    {
                        // There is not AddRange currently. This will notify the
                        // collection change every time an element is added.
                        FilteredExpenses.Add(e);
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
        public async Task GoToExpenseDetailsAsync(ExpenseView expense)
        {
            if (expense is null) return;

            await Shell.Current.GoToAsync($"{nameof(ExpenseDetailsPage)}", true,
                new Dictionary<string, object>() {
                    { "Expense", expense }
                });

            SearchText = "";
        }

        [RelayCommand]
        public async Task DeleteExpenseAsync(ExpenseView expense)
        {
            if (expense != null)
            {
                var ret = await _deleteExpenseUseCase.ExecuteAsync(expense.Id);
                if (ret) _cachedExpenses.Remove(expense);

                SearchText = "";
            }
        }

        [RelayCommand]
        public async Task SearchExpenseAsync()
        {
            var list = SearchText?.Length <= 0 ?
                            _cachedExpenses :
                            _cachedExpenses.Where(t => t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            if (FilteredExpenses.Count > 0) FilteredExpenses.Clear();

            foreach (var t in list)
            {
                FilteredExpenses.Add(t);
            }
        }

        [RelayCommand]
        public async Task AddExpenseAsync()
        {

        }
    }
}
