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

        #endregion
    }
}
 