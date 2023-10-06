using ShihBooks.ViewModels;

namespace ShihBooks.Views;

public partial class ManageMerchantsPage : ContentPage
{
    private readonly ManageMerchantsViewModel _manageMerchantsViewModel;

    public ManageMerchantsPage(ManageMerchantsViewModel manageMerchantsViewModel)
	{
		InitializeComponent();
        _manageMerchantsViewModel = manageMerchantsViewModel;
        BindingContext = _manageMerchantsViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _manageMerchantsViewModel.GetEntitiesAsync();
    }
}