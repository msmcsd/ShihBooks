using ShihBooks.Core.Incomes;
using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.Interfaces.Incomes;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.Incomes
{
    public class UpdateIncomeUseCase : IUpdateIncomeUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public UpdateIncomeUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<StatusResponse> ExecuteAsync(Income income)
        {
            return await _expensesDataStore.UpdateIncomeAsync(income);
        }
    }
}
