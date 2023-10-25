using ShihBooks.ViewModels;

namespace ShihBooks.Views.Incomes;

public partial class ManageIncomeRecipientsPage : ManageEntitiesBasePage
{
    private readonly ManageIncomeRecipientsViewModel _manageIncomeRecipientsViewModel;

    public ManageIncomeRecipientsPage(ManageIncomeRecipientsViewModel manageIncomeRecipientsViewModel)
	{
		InitializeComponent();
        _manageIncomeRecipientsViewModel = manageIncomeRecipientsViewModel;
        BindingContext = _manageIncomeRecipientsViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _manageIncomeRecipientsViewModel.GetEntitiesAsync();
    }
}