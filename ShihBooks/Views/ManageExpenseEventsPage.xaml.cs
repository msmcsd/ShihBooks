using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ManageExpenseEventsPage : ContentPage
{
    private readonly ManageExpenseEventsViewModel _manageExpenseEventsViewModel;

    public ManageExpenseEventsPage(ManageExpenseEventsViewModel manageExpenseEventsViewModel)
	{
		InitializeComponent();
        _manageExpenseEventsViewModel = manageExpenseEventsViewModel;
        BindingContext = _manageExpenseEventsViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _manageExpenseEventsViewModel.GetEntitiesAsync();
    }
}