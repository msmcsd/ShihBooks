using CommunityToolkit.Maui.Views;

namespace ShihBooks.Views;

public partial class ManageItemPopupPage : Popup
{
	public ManageItemPopupPage()
	{
		InitializeComponent();
	}

    public ManageItemPopupPage(string defaultItemName)
    {
        InitializeComponent();
        itemName.Text = defaultItemName;
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
		Close();
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(itemName.Text))
        {
            Close();
            return;
        }

        Close(itemName.Text);
    }
}