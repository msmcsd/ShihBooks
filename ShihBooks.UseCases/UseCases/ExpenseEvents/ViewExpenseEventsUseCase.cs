﻿using ShihBooks.Core;
using ShihBooks.UseCases.Interfaces.ExpenseEvents;
using ShihBooks.UseCases.PluginInterfaces;

namespace ShihBooks.UseCases.UseCases.ExpenseEvents
{
    public class ViewExpenseEventsUseCase : IViewExpenseEventsUseCase
    {
        private readonly IExpensesDataStore _expensesDataStore;

        public ViewExpenseEventsUseCase(IExpensesDataStore expensesDataStore)
        {
            _expensesDataStore = expensesDataStore;
        }

        public async Task<List<CoreEntity>> ExecuteAsync()
        {
            var events = await _expensesDataStore.GetExpenseEventsAsync();
            return events.ConvertAll(e => new CoreEntity
            {
                Id = e.Id,
                Name = e.Name
            });
        }
    }
}
