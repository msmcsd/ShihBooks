using ShihBooks.Core;
using ShihBooks.ViewModels;
using System.Reflection.Metadata;

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

        if (_expenseDetailsViewModel.Expense == null)
        {
            _expenseDetailsViewModel.Expense = new() { ExpenseDate = DateTime.Today};
            _expenseDetailsViewModel.IsNewExpense = true;
            return;
        }

        _expenseDetailsViewModel.SelectedMerchant = _expenseDetailsViewModel.Merchants.FirstOrDefault(m => m.Id == _expenseDetailsViewModel.Expense.MerchantId);
        _expenseDetailsViewModel.SelectedType = _expenseDetailsViewModel.ExpenseTypes.FirstOrDefault(m => m.Id == _expenseDetailsViewModel.Expense.ExpenseTypeId);
        if (_expenseDetailsViewModel.Expense.TagId != null)
            _expenseDetailsViewModel.SelectedTag = _expenseDetailsViewModel.ExpenseTags.FirstOrDefault(m => m.Id == _expenseDetailsViewModel.Expense.TagId);

        if (_expenseDetailsViewModel.Expense.EventId != null)
            _expenseDetailsViewModel.SelectedEvent = _expenseDetailsViewModel.ExpenseEvents.FirstOrDefault(m => m.Id == _expenseDetailsViewModel.Expense.EventId);
        
    }

    private void MerchantPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker?.SelectedItem != null)
        {
            Merchant merchant = picker.SelectedItem as Merchant;
            _expenseDetailsViewModel.SelectedMerchant = merchant;
            _expenseDetailsViewModel.Expense.MerchantId = merchant.Id;
            _expenseDetailsViewModel.Expense.MerchantImageUrl = merchant.ImageUrl;
        }
        else
        {
            _expenseDetailsViewModel.Expense.MerchantId = null;
            _expenseDetailsViewModel.Expense.MerchantImageUrl = null;
        }
    }

    private void ExpenseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        _expenseDetailsViewModel.Expense.ExpenseTypeId = picker?.SelectedItem == null ? null : (picker.SelectedItem as ExpenseType).Id;
    }

    private void ExpenseTag_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        _expenseDetailsViewModel.Expense.TagId = picker?.SelectedItem == null ? null : (picker.SelectedItem as ExpenseTag).Id;
    }

    private void ExpenseEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        _expenseDetailsViewModel.Expense.EventId = picker?.SelectedItem == null ? null : (picker.SelectedItem as ExpenseEvent).Id;
    }
}