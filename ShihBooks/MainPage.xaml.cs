using ShihBooks.ViewModels;
using ShihBooks.Views;

namespace ShihBooks;

public partial class MainPage : ContentPage
{
    //int count = 0;

    public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
        BindingContext = mainPageViewModel;
    }
}

