using CommunityToolkit.Maui.Views;
using ShihBooks.ViewModels;
using ShihBooks.Core;

namespace ShihBooks.Views;

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
        await _manageExpenseTypesViewModel.GetAllExpenseTypesAsync();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        var ret = await this.ShowPopupAsync(new ManageItemPopupPage(true, "Enter the type name:", ""));
        if (ret is null)
        {
            return;
        }

        await _manageExpenseTypesViewModel.AddExpenseType(ret as string);
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        if (itemList.SelectedItem == null) return;

        var selectedItem = itemList.SelectedItem as ExpenseType;
        var origItemName = selectedItem.Name;

        var ret = await this.ShowPopupAsync(new ManageItemPopupPage(false, "Enter the new type name:", origItemName));
        if (ret is null)
        {
            return;
        }

        var newItemName = ret as string;
        if (newItemName != origItemName)
        {
            var origSearchText = _manageExpenseTypesViewModel.SearchText;

            await _manageExpenseTypesViewModel.UpdateExpenseType(selectedItem.Id, newItemName);

            if (!string.IsNullOrEmpty(origSearchText))
            {
                _manageExpenseTypesViewModel.SearchText = null;
            }
        }
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        await _manageExpenseTypesViewModel.PerformSearchAsync();
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {

    }
}