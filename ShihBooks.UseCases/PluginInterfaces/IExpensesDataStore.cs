﻿using ShihBooks.Core;

namespace ShihBooks.UseCases.PluginInterfaces
{
    public interface IExpensesDataStore
    {
        Task<List<ExpenseView>> GetExpenses(int year, int month);

        Task<List<ExpenseType>> GetExpenseTypesAsync();

        Task<List<Merchant>> GetMerchantsAsync();

        Task<List<ExpenseTag>> GetExpenseTagsAsync();

        Task<bool> AddExpenseTag(string tagName);

        Task<bool> UpdateExpenseTag(int tagId, string tagName);

        Task <bool> UpdateExpense(Expense expense);

        Task<List<ExpenseEvent>> GetExpenseEventsAsync();

        Task<bool> DeleteExpense(int expenseId);

        Task<int> DeleteExpenseTagAsync(int tagId);

        Task<bool> AddExpenseType(string name);

        Task<bool> UpdateExpenseType(int id, string newTypeName);

        Task<int> DeleteExpenseTypeAsync(int id);
    }
}
 