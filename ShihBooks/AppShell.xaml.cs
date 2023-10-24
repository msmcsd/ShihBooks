using ShihBooks.Views;
using ShihBooks.Views.Incomes;
using ShihBooks.Views.Expenses;

namespace ShihBooks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ExpensesPage), typeof(ExpensesPage));
		Routing.RegisterRoute(nameof(ExpenseDetailsPage), typeof(ExpenseDetailsPage));

        Routing.RegisterRoute(nameof(IncomesPage), typeof(IncomesPage));
        Routing.RegisterRoute(nameof(IncomeDetailsPage), typeof(IncomeDetailsPage));
    }
}
