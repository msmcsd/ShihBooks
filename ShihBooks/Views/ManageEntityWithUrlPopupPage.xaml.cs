using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using ShihBooks.Core;

namespace ShihBooks.Views;

public partial class ManageEntityWithUrlPopupPage : Popup
{
    public ManageEntityWithUrlPopupPage(string entityInfo)
    {
        InitializeComponent();
        EntityInfo.Text = $"{entityInfo} Info";
    }

    public ManageEntityWithUrlPopupPage(string entityInfo, string defaultEntityName, string defaultImageUrl) : this(entityInfo)
	{
        NameEntry.Text = defaultEntityName;
		UrlEntry.Text = defaultImageUrl;
		AddButton.Text = "Update";
        ImageUrl.Source = defaultImageUrl;
	}

    private void Add_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            var toast = Toast.Make("Name is required.", ToastDuration.Short);
            toast.Show();
            return;
        }

        Close(new CoreEntityWithUrl { Name = NameEntry.Text.Trim(), ImageUrl = UrlEntry.Text?.Trim() });
    }

    private void RemoveName_Clicked(object sender, EventArgs e)
    {
        NameEntry.Text = "";
    }

    private void RemoveUrl_Clicked(object sender, EventArgs e)
    {
        UrlEntry.Text = "";
    }
}