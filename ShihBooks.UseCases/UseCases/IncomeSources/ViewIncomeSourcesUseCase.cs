﻿using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.IncomeSources;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.IncomeSources
{
    public class ViewIncomeSourcesUseCase : IViewIncomeSourcesUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public ViewIncomeSourcesUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<List<ExpenseEntity>> ExecuteAsync()
        {
            var ret = await _expensesDataStore.GetIncomeSourcesAsync();
            return ret.ConvertAll(i => new ExpenseEntity
            {
                Id = i.Id,
                Name = i.Name
            });
        }
    }
}