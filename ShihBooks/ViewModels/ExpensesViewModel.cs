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

        public ObservableCollection<ExpenseView> Expenses { get; set; } = new();

        [ObservableProperty]
        private int _year;

        [ObservableProperty]
        private int _month;

        public ExpensesViewModel(IViewExpensesByMonthUseCase viewExpensesByMonthUseCase,
                                 IDeleteExpenseUseCase deleteExpenseUseCase)
        {
            _viewExpensesByMonthUseCase = viewExpensesByMonthUseCase;
            _deleteExpenseUseCase = deleteExpenseUseCase;
        }

        public async Task LoadExpensesByMonthAsync(int year, int month)
        {
            if (IsBusy) return;

            if (Expenses.Count > 0)
            {
                Expenses.Clear();
            }

            try
            {
                IsBusy = true;

                var expenses = await _viewExpensesByMonthUseCase.ExecuteAsync(year, month);

                if (expenses?.Count > 0)
                {
                    foreach (var e in expenses)
                    {
                        // There is not AddRange currently. This will notify the
                        // collection change every time an element is added.
                        Expenses.Add(e);
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
        }

        [RelayCommand]
        public async Task DeleteExpenseAsync(ExpenseView expense)
        {
            if (expense != null)
            {
                var ret = await _deleteExpenseUseCase.ExecuteAsync(expense.Id);
                if (ret) Expenses.Remove(expense);
            }
        }
    }
}
