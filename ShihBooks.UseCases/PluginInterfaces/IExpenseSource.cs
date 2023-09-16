using ShihBooks.Core;

namespace ShihBooks.UseCases.PluginInterfaces
{
    public interface IExpenseSource
    {
        Task<List<Expense>> GetExpenses(int year, int month);

        Task<List<ExpenseType>> GetExpenseTypesAsync();

        Task<List<Merchant>> GetMerchantsAsync();
    }
}
