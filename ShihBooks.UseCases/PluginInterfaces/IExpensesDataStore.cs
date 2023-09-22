using ShihBooks.Core;

namespace ShihBooks.UseCases.PluginInterfaces
{
    public interface IExpensesDataStore
    {
        Task<List<ExpenseView>> GetExpenses(int year, int month);

        Task<List<ExpenseType>> GetExpenseTypesAsync();

        Task<List<Merchant>> GetMerchantsAsync();

        Task<List<ExpenseTag>> GetExpenseTagsAsync();

        Task SavExpenseTag(string tagName);

        Task<bool> UpdateExpenseTag(int tagId, string tagName);
    }
}
