using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace ShihBooks.Views;

public partial class ManageItemPopupPage : Popup
{
	public ManageItemPopupPage()
	{
		InitializeComponent();
	}

    public ManageItemPopupPage(bool isAdd, string instruction, string defaultItemName)
    {
        InitializeComponent();
        instructionLabel.Text = instruction;
        itemName.Text = defaultItemName;
        addButton.Text = isAdd ? "Add" : "Update";
        if (!isAdd)
        {
            itemName.CursorPosition = 0;
            itemName.SelectionLength = itemName.Text.Length;
        }
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
		Close();
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(itemName.Text))
        {
            var toast = Toast.Make("Name is required.", ToastDuration.Short);
            toast.Show();
            return;
        }

        Close(itemName.Text);
    }
}