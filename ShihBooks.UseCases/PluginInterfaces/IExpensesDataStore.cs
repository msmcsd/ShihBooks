using ShihBooks.Core;

namespace ShihBooks.UseCases.PluginInterfaces
{
    public interface IExpensesDataStore
    {
        #region Expense

        Task<List<ExpenseView>> GetExpensesAsync(int year, int month);

        Task<bool> UpdateExpenseAsync(Expense expense);

        Task<bool> DeleteExpenseAsync(int expenseId);

        #endregion

        Task<List<Merchant>> GetMerchantsAsync();


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
    }
}
 