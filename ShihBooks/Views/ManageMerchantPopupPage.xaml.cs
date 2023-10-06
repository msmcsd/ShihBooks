using CommunityToolkit.Maui.Views;
using ShihBooks.Core;

namespace ShihBooks.Views;

public partial class ManageMerchantPopupPage : Popup
{
    public ManageMerchantPopupPage()
    {
        InitializeComponent();
    }

    public ManageMerchantPopupPage(string defaultMerchantName, string defaultImageUrl)
	{
		InitializeComponent();

		nameEntry.Text = defaultMerchantName;
		urlEntry.Text = defaultImageUrl;
		addButton.Text = "Update";
        MerchantImageUrl.Source = defaultImageUrl;
	}

    private void Add_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nameEntry.Text))
        {
            Close();
            return;
        }

        Close(new Merchant { Name = nameEntry.Text.Trim(), ImageUrl=urlEntry.Text?.Trim()});
    }

    private void RemoveName_Clicked(object sender, EventArgs e)
    {
        nameEntry.Text = "";
    }

    private void RemoveUrl_Clicked(object sender, EventArgs e)
    {
        urlEntry.Text = "";
    }
}