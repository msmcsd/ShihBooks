using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ShihBooks.Core;
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

    private async void AddTag_Clicked(object sender, EventArgs e)
    {
        var ret= await this.ShowPopupAsync(new ManageItemPopupPage(true, "Enter the tag name:", ""));
        if (ret is null)
        {
            return;
        }

        await _manageExpenseTagsViewModel.SaveExpenseTag(ret as string);
    }

    private async void EditTag_Clicked(object sender, EventArgs e)
    {
        if (tagList.SelectedItem == null) return;

        var tag = tagList.SelectedItem as ExpenseTag;
        var origTagName = tag.Name;

        var ret = await this.ShowPopupAsync(new ManageItemPopupPage(false, "Enter the new tag name:", origTagName));
        if (ret is null)
        {
            return;
        }

        var newTagName = ret as string;
        if (newTagName != origTagName)
        {
            await _manageExpenseTagsViewModel.UpdateExpenseTag(tag.Id, newTagName);
        }
    }
}