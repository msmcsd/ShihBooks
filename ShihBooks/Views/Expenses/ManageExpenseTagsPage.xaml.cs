using ShihBooks.ViewModels;

namespace ShihBooks.Views.Expenses;

public partial class ManageExpenseTagsPage : ManageEntitiesBasePage
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
        await _manageExpenseTagsViewModel.GetEntitiesAsync();
    }
}