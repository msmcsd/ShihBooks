using Microsoft.Extensions.Logging;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases;
using ShihBooks.UseCases.Interfaces;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.ViewModels;
using ShihBooks.Views;

namespace ShihBooks;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddTransient<IViewExpensesByMonthUseCase, ViewExpensesByMonthUseCase>();

		builder.Services.AddSingleton<IExpenseSource, ExpenseSourceInMemory>();
        
		builder.Services.AddSingleton<ExpensesViewModel>();
		builder.Services.AddSingleton<MainPageViewModel>();   
		
		builder.Services.AddSingleton<ExpensesPage>();
		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
