using ShihBooks.Core;
using ShihBooks.Core.Expenses;
using ShihBooks.Core.Incomes;
using ShihBooks.Core.StatusResponses;

namespace ShihBooks.UseCases.PluginInterfaces
{
    public interface IExpensesDataStore
    {
        #region Expense

        Task<List<ExpenseView>> GetExpensesAsync(int year, int month);

        Task<StatusResponse> AddExpenseAsync(Expense expense);

        Task<StatusResponse> UpdateExpenseAsync(Expense expense);

        Task<StatusResponse> DeleteExpenseAsync(int expenseId);

        #endregion

        #region Merchant

        Task<List<Merchant>> GetMerchantsAsync();

        Task<StatusResponse> AddMerchantAsync(string merchantName, string imageUrl);

        Task<StatusResponse> UpdateMerchantAsync(int id, string merchantName, string imageUrl);

        Task<StatusResponse> DeleteMerchantAsync(int id);

        #endregion

        #region Expense Tag

        Task<List<ExpenseTag>> GetExpenseTagsAsync();

        Task<StatusResponse> AddExpenseTagAsync(string tagName);

        Task<StatusResponse> UpdateExpenseTagAsync(int tagId, string tagName);

        Task<StatusResponse> DeleteExpenseTagAsync(int tagId);

        #endregion

        #region Expense Type

        Task<List<ExpenseType>> GetExpenseTypesAsync();

        Task<StatusResponse> AddExpenseTypeAsync(string name);

        Task<StatusResponse> UpdateExpenseTypeAsync(int id, string newTypeName);

        Task<StatusResponse> DeleteExpenseTypeAsync(int id);

        #endregion

        #region Expense Event

        Task<List<ExpenseEvent>> GetExpenseEventsAsync();

        Task<StatusResponse> AddExpenseEventAsync(string eventName);

        Task<StatusResponse> UpdateExpenseEventAsync(int id, string newEventName);

        Task<StatusResponse> DeleteExpenseEventAsync(int id);

        #endregion

        #region Income Source

        Task<List<IncomeSource>> GetIncomeSourcesAsync();

        Task<StatusResponse> AddIncomeSourceAsync(string sourceName, string imageUrl);

        Task<StatusResponse> DeleteIncomeSourceAsync(int id);

        Task<StatusResponse> UpdateIncomeSourceAsync(int id, string newSourceName, string imageUrl);

        #endregion

        #region Income

        Task<List<IncomeDetails>> GetIncomesAsync(int year, int month);

        Task<StatusResponse> AddIncomeAsync(Income income);

        Task<StatusResponse> UpdateIncomeAsync(Income income);

        Task<StatusResponse> DeleteIncomeAsync(int id);

        #endregion

        #region Income Recipient

        Task<List<IncomeRecipient>> GetIncomeRecipients();

        Task<StatusResponse> AddIncomeRecipientAsync(string name);

        Task<StatusResponse> UpdateIncomeRecipientAsync(int id, string name);

        Task<StatusResponse> DeleteIncomeRecipientAsync(int id);

        #endregion
    }
}
 