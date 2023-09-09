using CommunityToolkit.Mvvm.Input;
using ShihBooks.Views;

namespace ShihBooks.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {

        [RelayCommand]
        public async Task ShowCurrentMonthExpenses()
        {
            if (IsBusy) return;

            await Shell.Current.GoToAsync($"{nameof(ExpensesPage)}?year={DateTime.Now.Year}&month={DateTime.Now.Month}");
        }
    }
}
