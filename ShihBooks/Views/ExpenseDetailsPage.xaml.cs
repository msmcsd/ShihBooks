using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ExpenseDetailsPage : ContentPage
{
	public ExpenseDetailsPage(ExpenseDetailsViewModel expenseDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = expenseDetailsViewModel;
	}
}