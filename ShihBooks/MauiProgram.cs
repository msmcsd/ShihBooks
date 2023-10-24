using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.Plugins.DataStore.Sqlite;
using ShihBooks.Plugins.DataStore.WebApi;
using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.Interfaces.Expenses;
using ShihBooks.UseCases.Interfaces.ExpenseTags;
using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using ShihBooks.UseCases.Interfaces.Incomes;
using ShihBooks.UseCases.Interfaces.IncomeSources;
using ShihBooks.UseCases.Interfaces.Merchants;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.UseCases.UseCases.ExpenseEvents;
using ShihBooks.UseCases.UseCases.Expenses;
using ShihBooks.UseCases.UseCases.ExpenseTags;
using ShihBooks.UseCases.UseCases.ExpenseTypes;
using ShihBooks.UseCases.UseCases.IncomeSources;
using ShihBooks.UseCases.UseCases.Merchants;
using ShihBooks.UseCases.UseCases.Incomes;
using ShihBooks.ViewModels;
using ShihBooks.Views;
using ShihBooks.Views.Controls;
using ShihBooks.Views.Incomes;
using ShihBooks.Views.Expenses;
using ShihBooks.UseCases.Interfaces.IncomeRecipients;
using ShihBooks.UseCases.UseCases.IncomeRecipients;

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


        // Expense
        builder.Services.AddTransient<IViewExpensesByMonthUseCase, ViewExpensesByMonthUseCase>();
        builder.Services.AddTransient<IAddExpenseUseCase, AddExpenseUseCase>();
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

		// Income Source
        builder.Services.AddTransient<IViewIncomeSourcesUseCase, ViewIncomeSourcesUseCase>();
        builder.Services.AddTransient<IAddIncomeSourceUseCase, AddIncomeSourceUseCase>();
        builder.Services.AddTransient<IUpdateIncomeSourceUseCase, UpdateIncomeSourceUseCase>();
        builder.Services.AddTransient<IDeleteIncomeSourceUseCase, DeleteIncomeSourceUseCase>();

        // Income
        builder.Services.AddTransient<IViewIncomesByMonthUseCase, ViewIncomesByMonthUseCase>();
        builder.Services.AddTransient<IAddIncomeUseCase, AddIncomeUseCase>();
        builder.Services.AddTransient<IUpdateIncomeUseCase, UpdateIncomeUseCase>();
        builder.Services.AddTransient<IDeleteIncomeUseCase, DeleteIncomeUseCase>();

        // Income Recipient
        builder.Services.AddTransient<IViewIncomeRecipientsUseCase, ViewIncomeRecipientsUseCase>();
        builder.Services.AddTransient<IAddIncomeRecipientUseCase, AddIncomeRecipientUseCase>();
        builder.Services.AddTransient<IUpdateIncomeRecipientUseCase, UpdateIncomeRecipientUseCase>();
        builder.Services.AddTransient<IDeleteIncomeRecipientUseCase, DeleteIncomeRecipientUseCase>();

        // Merchant
        builder.Services.AddTransient<IViewMerchantsUseCase, ViewMerchantsUseCase>();
        builder.Services.AddTransient<IAddMerchantUseCase, AddMerchantUseCase>();
        builder.Services.AddTransient<IUpdateMerchantUseCase, UpdateMerchantUseCase>();
        builder.Services.AddTransient<IDeleteMerchantUseCase, DeleteMerchantUseCase>();

		builder.Services.AddSingleton<IExpensesDataStore, InMemoryExpensesDataStore>();
		//builder.Services.AddSingleton<IExpensesDataStore, SqliteExpensesDataStore>();
		//builder.Services.AddSingleton<IExpensesDataStore, WebApiExpensesDataStore>();

		// View Models
		builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<ExpensesViewModel>();
		builder.Services.AddTransient<ExpenseDetailsViewModel>();
		builder.Services.AddTransient<ManageExpenseTagsViewModel>();
		builder.Services.AddTransient<ManageExpenseTypesViewModel>();
		builder.Services.AddTransient<ManageExpenseEventsViewModel>();

		builder.Services.AddTransient<ManageIncomeSourcesViewModel>();
		builder.Services.AddTransient<ManageIncomeRecipientsViewModel>();
		builder.Services.AddTransient<IncomeDetailsViewModel>();

		builder.Services.AddTransient<ManageMerchantsViewModel>();

		builder.Services.AddTransient<IncomesViewModel>();
		//builder.Services.AddTransient<ExpenseDatePickerViewModel>();

		// Pages
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ExpensesPage>();
		builder.Services.AddTransient<ExpenseDetailsPage>();
		builder.Services.AddTransient<ManageExpenseTagsPage>();
		builder.Services.AddTransient<ManageExpenseTypesPage>();
		builder.Services.AddTransient<ManageExpenseEventsPage>();
		builder.Services.AddTransient<ManageIncomeSourcesPage>();
		builder.Services.AddTransient<ManageIncomeRecipientsPage>();
		builder.Services.AddTransient<IncomeDetailsPage>();
		builder.Services.AddTransient<ManageMerchantsPage>();

		builder.Services.AddTransient<IncomesPage>();
		builder.Services.AddTransient<IncomeDetailsPage>();

		builder.Services.AddTransient<ExpenseDatePicker>();

		return builder.Build();
	}
}
