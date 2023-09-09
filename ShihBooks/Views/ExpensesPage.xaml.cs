using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ExpensesPage : ContentPage
{
    private readonly ExpensesViewModel _expensesViewModel;

    public ExpensesPage(ExpensesViewModel expensesViewModel)
	{
		InitializeComponent();
        _expensesViewModel = expensesViewModel;
        BindingContext = _expensesViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _expensesViewModel.LoadExpensesByMonthAsync(_expensesViewModel.Year, _expensesViewModel.Month);
    }
}