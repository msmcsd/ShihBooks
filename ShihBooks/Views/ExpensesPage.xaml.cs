using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ExpensesPage : ContentPage
{
    private readonly ExpensesViewModel _expenseViewModel;

    public ExpensesPage(ExpensesViewModel expenseViewModel)
	{
		InitializeComponent();
        _expenseViewModel = expenseViewModel;
        BindingContext = _expenseViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _expenseViewModel.LoadExpensesByMonthAsync(2023, 8);
    }
}