using ShihBooks.ViewModels;

namespace ShihBooks.Views.Expenses;

public partial class ManageExpenseTypesPage : ContentPage
{
    private readonly ManageExpenseTypesViewModel _manageExpenseTypesViewModel;

    public ManageExpenseTypesPage(ManageExpenseTypesViewModel manageExpenseTypesViewModel)
	{
		InitializeComponent();
        _manageExpenseTypesViewModel = manageExpenseTypesViewModel;
        BindingContext = manageExpenseTypesViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _manageExpenseTypesViewModel.GetEntitiesAsync();
    }
}