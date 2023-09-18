using Microsoft.Extensions.Logging;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.Plugins.DataStore.Sqlite;
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
		builder.Services.AddTransient<IViewExpenseTypesUseCase, ViewExpenseTypesUseCase>();
		builder.Services.AddTransient<IViewMerchantsUseCase, ViewMerchantsUseCase>();
		builder.Services.AddTransient<IViewExpenseTagsUseCase, ViewExpenseTagsUseCase>();

		//builder.Services.AddSingleton<IExpensesDataStore, InMemoryExpensesDataStore>();
		builder.Services.AddSingleton<IExpensesDataStore, SqliteExpensesDataStore>();

		builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<ExpensesViewModel>();
		builder.Services.AddTransient<ExpenseDetailsViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ExpensesPage>();
		builder.Services.AddTransient<ExpenseDetailsPage>();

		return builder.Build();
	}
}
