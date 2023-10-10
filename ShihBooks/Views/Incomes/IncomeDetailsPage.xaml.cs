using ShihBooks.ViewModels;
using ShihBooks.Core.Incomes;
using ShihBooks.Core;

namespace ShihBooks.Views.Incomes;

public partial class IncomeDetailsPage : ContentPage
{
    private readonly IncomeDetailsViewModel _incomeDetailsViewModel;

    public IncomeDetailsPage(IncomeDetailsViewModel incomeDetailsViewModel)
	{
		InitializeComponent();
        _incomeDetailsViewModel = incomeDetailsViewModel;
        BindingContext = _incomeDetailsViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_incomeDetailsViewModel.Income == null)
        {
            _incomeDetailsViewModel.Income = new IncomeDetails { IncomeDate = DateTime.Now };
            _incomeDetailsViewModel.IsNewIncome = true;
            return;
        }

        _incomeDetailsViewModel.SelectedIncomeRecipient = _incomeDetailsViewModel.IncomeRecipients.FirstOrDefault(r => r.Id == _incomeDetailsViewModel.Income.RecipientId);
        _incomeDetailsViewModel.SelectedIncomeSource = _incomeDetailsViewModel.IncomeSources.FirstOrDefault(r => r.Id == _incomeDetailsViewModel.Income.SourceId);
    }

    private void IncomeSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        _incomeDetailsViewModel.Income.SourceId = ((IncomeSource)picker?.SelectedItem).Id;
    }

    private void IncomeRecipient_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        _incomeDetailsViewModel.Income.RecipientId = ((IncomeRecipient)picker?.SelectedItem).Id;
    }
}