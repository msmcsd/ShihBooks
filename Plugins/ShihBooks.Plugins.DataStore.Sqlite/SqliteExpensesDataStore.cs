using ShihBooks.Core;
using ShihBooks.UseCases.PluginInterfaces;
using SQLite;

namespace ShihBooks.Plugins.DataStore.Sqlite
{
    // All the code in this file is included in all platforms.
    public class SqliteExpensesDataStore : IExpensesDataStore
    {
        private SQLiteAsyncConnection _db;

        public SqliteExpensesDataStore()
        {
            _db = new SQLiteAsyncConnection(Constants.DatabasePath);

            _db.CreateTableAsync<ExpenseTag>();
            _db.CreateTableAsync<Merchant>();
            _db.CreateTableAsync<ExpenseType>();
            _db.CreateTableAsync<Expense>();
        }
        public async Task<List<ExpenseView>> GetExpenses(int year, int month)
        {
            //return await _db.QueryAsync("SELECT e.* FROM Expense e ", "1");
            return null;
        }

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return await _db.Table<ExpenseTag>().ToListAsync();
        }

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await _db.Table<ExpenseType>().ToListAsync();
        }

        public async Task<List<Merchant>> GetMerchantsAsync()
        {
            return await _db.Table<Merchant>().ToListAsync();
        }
    }
}