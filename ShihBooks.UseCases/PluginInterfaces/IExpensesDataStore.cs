using ShihBooks.Core;
using ShihBooks.Core.Expenses;
using ShihBooks.Core.Incomes;

namespace ShihBooks.UseCases.PluginInterfaces
{
    public interface IExpensesDataStore
    {
        #region Expense

        Task<List<ExpenseView>> GetExpensesAsync(int year, int month);

        Task <bool>AddExpenseAsync(Expense expense);

        Task<bool> UpdateExpenseAsync(Expense expense);

        Task<bool> DeleteExpenseAsync(int expenseId);

        #endregion

        #region Merchant

        Task<List<Merchant>> GetMerchantsAsync();

        Task<bool> AddMerchantAsync(string merchantName, string imageUrl);

        Task<bool> UpdateMerchantAsync(int id, string merchantName, string imageUrl);

        Task<int> DeleteMerchantAsync(int id);

        #endregion

        #region Expense Tag

        Task<List<ExpenseTag>> GetExpenseTagsAsync();

        Task<bool> AddExpenseTagAsync(string tagName);

        Task<bool> UpdateExpenseTagAsync(int tagId, string tagName);

        Task<int> DeleteExpenseTagAsync(int tagId);

        #endregion

        #region Expense Type

        Task<List<ExpenseType>> GetExpenseTypesAsync();

        Task<bool> AddExpenseTypeAsync(string name);

        Task<bool> UpdateExpenseTypeAsync(int id, string newTypeName);

        Task<int> DeleteExpenseTypeAsync(int id);

        #endregion

        #region Expense Event

        Task<List<ExpenseEvent>> GetExpenseEventsAsync();

        Task<bool> AddExpenseEventAsync(string eventName);

        Task<bool> UpdateExpenseEventAsync(int id, string newEventName);

        Task<int> DeleteExpenseEventAsync(int id);

        #endregion

        #region Income Source

        Task<bool> AddIncomeSourceAsync(string sourceName);

        Task<int> DeleteIncomeSourceAsync(int id);

        Task<bool> UpdateIncomeSourceAsync(int id, string newSourceName);

        Task<List<IncomeSource>> GetIncomeSourcesAsync();

        #endregion

        #region Income

        Task<List<IncomeDetails>> GetIncomesAsync(int year, int month);

        Task<bool> AddIncomeAsync(Income income);

        Task<bool> UpdateIncomeAsync(Income income);


        Task<int> DeleteIncomeAsync(int id);

        #endregion

        #region Income Recipient

        Task<List<IncomeRecipient>> GetIncomeRecipients();

        Task<bool> AddIncomeRecipientAsync(string name);

        Task<bool> UpdateIncomeRecipientAsync(int id, string name);

        Task<int> DeleteIncomeRecipientAsync(int id);

        #endregion
    }
}
 