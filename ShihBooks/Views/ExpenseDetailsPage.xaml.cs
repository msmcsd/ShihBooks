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
        _expenseDetailsViewModel.SelectedCategory = _expenseDetailsViewModel.ExpenseCategories.FirstOrDefault(m => m.Id == _expenseDetailsViewModel.Expense.CategoryId);
        if (_expenseDetailsViewModel.Expense.TagId != null)
            _expenseDetailsViewModel.SelectedTag = _expenseDetailsViewModel.ExpenseTags.FirstOrDefault(m => m.Id == _expenseDetailsViewModel.Expense.TagId);
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