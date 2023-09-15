using ShihBooks.Core;
using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ExpenseDetailsPage : ContentPage
{
    private readonly ExpenseDetailsViewModel _expenseDetailsViewModel;

    public ExpenseDetailsPage(ExpenseDetailsViewModel expenseDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = expenseDetailsViewModel;
        _expenseDetailsViewModel = expenseDetailsViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _expenseDetailsViewModel.SelectedMerchant = _expenseDetailsViewModel.Merchants.FirstOrDefault(m => m.Id == _expenseDetailsViewModel.Expense.MerchantId);
    }

    private void MerchantPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker?.SelectedItem != null)
        {
            Merchant merchant = picker.SelectedItem as Merchant;
            _expenseDetailsViewModel.SelectedMerchant = merchant;
        }
    }
}