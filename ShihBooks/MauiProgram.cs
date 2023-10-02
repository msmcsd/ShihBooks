using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.Plugins.DataStore.Sqlite;
using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.Interfaces.Expenses;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.UseCases.UseCases.ExpenseEvents;
using ShihBooks.UseCases.UseCases.Expenses;
using ShihBooks.UseCases.UseCases.ExpenseTags;
using ShihBooks.UseCases.UseCases.ExpenseTypes;
using ShihBooks.UseCases.UseCases.Merchants;
using ShihBooks.ViewModels;
using ShihBooks.Views;
using ShihBooks.Views.Controls;

namespace ShihBooks;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
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

        // Expense
        builder.Services.AddTransient<IViewExpenseEventsUseCase, ViewExpenseEventsUseCase>();
        builder.Services.AddTransient<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        builder.Services.AddTransient<IDeleteExpenseUseCase, DeleteExpenseUseCase>();

		// Expense Tag
        builder.Services.AddTransient<IViewExpenseTagsUseCase, ViewExpenseTagsUseCase>();
        builder.Services.AddTransient<IAddExpenseTagUseCase, AddExpenseTagUseCase>();
        builder.Services.AddTransient<IUpdateExpenseTagUseCase, UpdateExpenseTagUseCase>();
        builder.Services.AddTransient<IDeleteExpenseTagUseCase, DeleteExpenseTagUseCase>();

		// Expense Type
        builder.Services.AddTransient<IViewExpenseTypesUseCase, ViewExpenseTypesUseCase>();
        builder.Services.AddTransient<IAddExpenseTypeUseCase, AddExpenseTypeUseCase>();
        builder.Services.AddTransient<IUpdateExpenseTypeUseCase, UpdateExpenseTypeUseCase>();
        builder.Services.AddTransient<IDeleteExpenseTypeUseCase, DeleteExpenseTypeUseCase>();

		// Expense Event
        builder.Services.AddTransient<IViewExpenseEventsUseCase, ViewExpenseEventsUseCase>();
        builder.Services.AddTransient<IAddExpenseEventUseCase, AddExpenseEventUseCase>();
        builder.Services.AddTransient<IUpdateExpenseEventUseCase, UpdateExpenseEventUseCase>();
        builder.Services.AddTransient<IDeleteExpenseEventUseCase, DeleteExpenseEventUseCase>();

        builder.Services.AddSingleton<IExpensesDataStore, InMemoryExpensesDataStore>();

		builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<ExpensesViewModel>();
		builder.Services.AddTransient<ExpenseDetailsViewModel>();
		builder.Services.AddTransient<ManageExpenseTagsViewModel>();
		builder.Services.AddTransient<ManageExpenseTypesViewModel>();
		builder.Services.AddTransient<ManageExpenseEventsViewModel>();
		//builder.Services.AddTransient<ExpenseDatePickerViewModel>();

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ExpensesPage>();
		builder.Services.AddTransient<ExpenseDetailsPage>();
		builder.Services.AddTransient<ManageExpenseTagsPage>();
		builder.Services.AddTransient<ManageExpenseTypesPage>();
		builder.Services.AddTransient<ManageExpenseEventsPage>();
		builder.Services.AddTransient<ExpenseDatePicker>();

		return builder.Build();
	}
}
