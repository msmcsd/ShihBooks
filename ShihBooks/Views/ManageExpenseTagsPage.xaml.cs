using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ManageExpenseTagsPage : ContentPage
{
    private readonly ManageExpenseTagsViewModel _manageExpenseTagsViewModel;

    public ManageExpenseTagsPage(ManageExpenseTagsViewModel manageExpenseTagsViewModel)
	{
		InitializeComponent();
        _manageExpenseTagsViewModel = manageExpenseTagsViewModel;
        BindingContext = _manageExpenseTagsViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _manageExpenseTagsViewModel.GetAllExpenseTagsAsync();
    }
}