using ShihBooks.UseCases.Interfaces.Incomes;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.Incomes
{
    public class DeleteIncomeUseCase : IDeleteIncomeUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public DeleteIncomeUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<int> ExecuteAsync(int id)
        {
            return await _expensesDataStore.DeleteIncomeAsync(id);
        }
    }
}
