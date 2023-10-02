using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ManageIncomeSourcesPage : ContentPage
{
    private readonly ManageIncomeSourcesViewModel _manageIncomeSourcesViewModel;

    public ManageIncomeSourcesPage(ManageIncomeSourcesViewModel manageIncomeSourcesViewModel)
	{
		InitializeComponent();
        _manageIncomeSourcesViewModel = manageIncomeSourcesViewModel;
        BindingContext = _manageIncomeSourcesViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _manageIncomeSourcesViewModel.GetExpenseEntitiesAsync();
    }
}