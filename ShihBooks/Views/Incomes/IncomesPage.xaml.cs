using ShihBooks.ViewModels;

namespace ShihBooks.Views.Incomes;

public partial class IncomesPage : ContentPage
{
    private readonly IncomesViewModel _incomesViewModel;

    public IncomesPage(IncomesViewModel incomesViewModel)
	{
		InitializeComponent();
        _incomesViewModel = incomesViewModel;
        BindingContext = _incomesViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _incomesViewModel.GetEntitiesAsync();
    }
}