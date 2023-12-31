﻿using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseTypes;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseTypes
{
    public class ViewExpenseTypesUseCase : IViewExpenseTypesUseCase
    {
        private readonly IExpensesDataStore _expenseSource;

        public ViewExpenseTypesUseCase(IExpensesDataStore expenseSource)
        {
            _expenseSource = expenseSource;
        }

        public async Task<List<CoreEntity>> ExecuteAsync()
        {
            var ret = await _expenseSource.GetExpenseTypesAsync();
            return ret.ConvertAll(t => new CoreEntity
            {
                Id = t.Id,
                Name = t.Name
            });
        }
    }
}
