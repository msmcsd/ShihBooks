using ShihBooks.Core;
using ShihBooks.UseCases.PluginInterfaces;
using System;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace ShihBooks.Plugins.DataStore.WebApi
{
    public class WebApiExpensesDataStore : IExpensesDataStore
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public WebApiExpensesDataStore()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        #region Expense

        public Task<List<ExpenseView>> GetExpensesAsync(int year, int month)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateExpenseAsync(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteExpenseAsync(int expenseId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Merchant

        public async Task<List<Merchant>> GetMerchantsAsync()
        {
            Uri uri = new($"{Constants.WebApiMerchantUrl}");
            var response = _client.GetAsync(uri);

            try
            {
                string content = await response.Result.Content.ReadAsStringAsync();

                var entities = JsonSerializer.Deserialize<List<Merchant>>(content, _serializerOptions);
                return entities ?? new List<Merchant>();
            }
            catch (Exception ex)
            {
                // TODO: Pass the exception to client.
                Console.WriteLine(ex.ToString());
            }

            return new List<Merchant>();
        }

        public async Task<bool> AddMerchantAsync(string merchantName, string imageUrl)
        {
            return await AddEntityAsync(new($"{Constants.WebApiMerchantUrl}?name={merchantName}&imageUrl={imageUrl}"));
        }

        public async Task<bool> UpdateMerchantAsync(int id, string name, string imageUrl)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiMerchantUrl}?id={id}&name={name}&imageUrl={imageUrl}"));
        }

        public async Task<int> DeleteMerchantAsync(int id)
        {
            return await DeleteEntitysync(new($"{Constants.WebApiMerchantUrl}?id={id}"), id);
        }

        #endregion

        #region Expense Tag

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return await GetEntitiesAsync<ExpenseTag>(new($"{Constants.WebApiExpenseTagUrl}"));
        }

        public async Task<bool> AddExpenseTagAsync(string name)
        {
            return await AddEntityAsync(new($"{Constants.WebApiExpenseTagUrl}?name={name}"));
        }

        public async Task<bool> UpdateExpenseTagAsync(int id, string name)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiExpenseTagUrl}?id={id}&name={name}"));
        }

        public async Task<int> DeleteExpenseTagAsync(int tagId)
        {
            return await DeleteEntitysync(new($"{Constants.WebApiExpenseTagUrl}?id={tagId}"), tagId);
        }

        #endregion

        #region Expense Type

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await GetEntitiesAsync<ExpenseType>(new($"{Constants.WebApiExpenseTypeUrl}"));
        }

        public async Task<bool> AddExpenseTypeAsync(string name)
        {
            return await AddEntityAsync(new($"{Constants.WebApiExpenseTypeUrl}?name={name}"));
        }

        public async Task<bool> UpdateExpenseTypeAsync(int id, string name)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiExpenseTypeUrl}?id={id}&name={name}"));
        }

        public async Task<int> DeleteExpenseTypeAsync(int id)
        {
            return await DeleteEntitysync(new($"{Constants.WebApiExpenseTypeUrl}?id={id}"), id);
        }

        #endregion

        #region Expense Event

        public async Task<List<ExpenseEvent>> GetExpenseEventsAsync()
        {
            return await GetEntitiesAsync<ExpenseEvent>(new($"{Constants.WebApiExpenseEventUrl}"));
        }

        public async Task<bool> AddExpenseEventAsync(string name)
        {
            return await AddEntityAsync(new($"{Constants.WebApiExpenseEventUrl}?name={name}"));
        }

        public async Task<bool> UpdateExpenseEventAsync(int id, string name)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiExpenseEventUrl}?id={id}&name={name}"));
        }

        public async Task<int> DeleteExpenseEventAsync(int id)
        {
            return await DeleteEntitysync(new($"{Constants.WebApiExpenseEventUrl}?id={id}"), id);
        }

        #endregion

        #region Income Source

        public async Task<List<IncomeSource>> GetIncomeSourcesAsync()
        {
            return await GetEntitiesAsync<IncomeSource>(new($"{Constants.WebApiIncomeSourceUrl}"));
        }

        public async Task<bool> AddIncomeSourceAsync(string name)
        {
            return await AddEntityAsync(new($"{Constants.WebApiIncomeSourceUrl}?name={name}"));
        }

        public async Task<bool> UpdateIncomeSourceAsync(int id, string name)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiIncomeSourceUrl}?id={id}&name={name}"));
        }

        public async Task<int> DeleteIncomeSourceAsync(int id)
        {
            return await DeleteEntitysync(new($"{Constants.WebApiIncomeSourceUrl}?id={id}"), id);
        }

        #endregion

        #region Generic Methods

        private async Task<List<T>> GetEntitiesAsync<T>(Uri uri) where T : CoreEntity
        {
            var response = _client.GetAsync(uri);

            try
            {
                string content = await response.Result.Content.ReadAsStringAsync();

                var entities = JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                return entities ?? new List<T>();
            }
            catch (Exception ex)
            {
                // TODO: Pass the exception to client.
                Console.WriteLine(ex.ToString());
            }

            return new List<T>();
        }

        private async Task<bool> AddEntityAsync(Uri uri)
        {
            var response = await _client.PostAsync(uri, null);
            return response.IsSuccessStatusCode;
        }

        private async Task<bool> UpdateEntityAsync(Uri uri)
        {
            var response = await _client.PutAsync(uri, null);
            return response.IsSuccessStatusCode;
        }

        private async Task<int> DeleteEntitysync(Uri uri, int id)
        {
            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return id;
            }

            return -1;
        }

        #endregion
    }
}