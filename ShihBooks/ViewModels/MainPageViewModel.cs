using CommunityToolkit.Mvvm.Input;
using ShihBooks.Views;

namespace ShihBooks.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {

        [RelayCommand]
        public async Task ShowCurrentMonthExpensesAsync()
        {
            if (IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(ExpensesPage)}?year={DateTime.Now.Year}&month={DateTime.Now.Month}");
        }

        [RelayCommand]
        public async Task AddExpenseAsync()
        {
            if (IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(ExpenseDetailsPage)}", true,
                new Dictionary<string, object>()
                {
                    { "Expense", null }
                });
        }

        [RelayCommand]
        public async Task ShowCurrentMonthIncomesAsync()
        {
            if (IsBusy) return;
        }

        [RelayCommand]
        public async Task AddIncomeAsync()
        {
            if (IsBusy) return;
        }
    }
}
