using ShihBooks.Core;
using ShihBooks.Core.Expenses;
using ShihBooks.Core.Incomes;
using ShihBooks.Core.StatusResponses;
using ShihBooks.UseCases.PluginInterfaces;
using System.Text.Json;

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

        public Task<StatusResponse> AddExpenseAsync(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> UpdateExpenseAsync(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> DeleteExpenseAsync(int expenseId)
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

        public async Task<StatusResponse> AddMerchantAsync(string merchantName, string imageUrl)
        {
            return await AddEntityAsync(new($"{Constants.WebApiMerchantUrl}?name={merchantName}&imageUrl={imageUrl}"));
        }

        public async Task<StatusResponse> UpdateMerchantAsync(int id, string name, string imageUrl)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiMerchantUrl}?id={id}&name={name}&imageUrl={imageUrl}"));
        }

        public async Task<StatusResponse> DeleteMerchantAsync(int id)
        {
            return await DeleteEntityAsync(new($"{Constants.WebApiMerchantUrl}?id={id}"));
        }

        #endregion

        #region Expense Tag

        public async Task<List<ExpenseTag>> GetExpenseTagsAsync()
        {
            return await GetEntitiesAsync<ExpenseTag>(new($"{Constants.WebApiExpenseTagUrl}"));
        }

        public async Task<StatusResponse> AddExpenseTagAsync(string name)
        {
            return await AddEntityAsync(new($"{Constants.WebApiExpenseTagUrl}?name={name}"));
        }

        public async Task<StatusResponse> UpdateExpenseTagAsync(int id, string name)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiExpenseTagUrl}?id={id}&name={name}"));
        }

        public async Task<StatusResponse> DeleteExpenseTagAsync(int tagId)
        {
            return await DeleteEntityAsync(new($"{Constants.WebApiExpenseTagUrl}?id={tagId}"));
        }

        #endregion

        #region Expense Type

        public async Task<List<ExpenseType>> GetExpenseTypesAsync()
        {
            return await GetEntitiesAsync<ExpenseType>(new($"{Constants.WebApiExpenseTypeUrl}"));
        }

        public async Task<StatusResponse> AddExpenseTypeAsync(string name)
        {
            return await AddEntityAsync(new($"{Constants.WebApiExpenseTypeUrl}?name={name}"));
        }

        public async Task<StatusResponse> UpdateExpenseTypeAsync(int id, string name)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiExpenseTypeUrl}?id={id}&name={name}"));
        }

        public async Task<StatusResponse> DeleteExpenseTypeAsync(int id)
        {
            return await DeleteEntityAsync(new($"{Constants.WebApiExpenseTypeUrl}?id={id}"));
        }

        #endregion

        #region Expense Event

        public async Task<List<ExpenseEvent>> GetExpenseEventsAsync()
        {
            return await GetEntitiesAsync<ExpenseEvent>(new($"{Constants.WebApiExpenseEventUrl}"));
        }

        public async Task<StatusResponse> AddExpenseEventAsync(string name)
        {
            return await AddEntityAsync(new($"{Constants.WebApiExpenseEventUrl}?name={name}"));
        }

        public async Task<StatusResponse> UpdateExpenseEventAsync(int id, string name)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiExpenseEventUrl}?id={id}&name={name}"));
        }

        public async Task<StatusResponse> DeleteExpenseEventAsync(int id)
        {
            return await DeleteEntityAsync(new($"{Constants.WebApiExpenseEventUrl}?id={id}"));
        }

        #endregion

        #region Income Source

        public async Task<List<IncomeSource>> GetIncomeSourcesAsync()
        {
            return await GetEntitiesAsync<IncomeSource>(new($"{Constants.WebApiIncomeSourceUrl}"));
        }

        public async Task<StatusResponse> AddIncomeSourceAsync(string name, string imageUrl)
        {
            return await AddEntityAsync(new($"{Constants.WebApiIncomeSourceUrl}?name={name}"));
        }

        public async Task<StatusResponse> UpdateIncomeSourceAsync(int id, string name, string imageUrl)
        {
            return await UpdateEntityAsync(new($"{Constants.WebApiIncomeSourceUrl}?id={id}&name={name}"));
        }

        public async Task<StatusResponse> DeleteIncomeSourceAsync(int id)
        {
            return await DeleteEntityAsync(new($"{Constants.WebApiIncomeSourceUrl}?id={id}"));
        }

        #endregion

        #region Income

        public Task<List<IncomeDetails>> GetIncomesAsync(int year, int month)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> AddIncomeAsync(Income income)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> UpdateIncomeAsync(Income income)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> DeleteIncomeAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Income Recipient

        public Task<List<IncomeRecipient>> GetIncomeRecipients()
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> AddIncomeRecipientAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> UpdateIncomeRecipientAsync(int id, string name)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> DeleteIncomeRecipientAsync(int id)
        {
            throw new NotImplementedException();
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

        private async Task<StatusResponse> AddEntityAsync(Uri uri)
        {
            var response = await _client.PostAsync(uri, null);

            return response.IsSuccessStatusCode ? 
                new StatusResponse(StatusCode.Success) : 
                new StatusResponse(StatusCode.Error, response.StatusCode.ToString());
        }

        private async Task<StatusResponse> UpdateEntityAsync(Uri uri)
        {
            var response = await _client.PutAsync(uri, null);

            return response.IsSuccessStatusCode ?
                new StatusResponse(StatusCode.Success) :
                new StatusResponse(StatusCode.Error, response.StatusCode.ToString());
        }

        private async Task<StatusResponse> DeleteEntityAsync(Uri uri)
        {
            var response = await _client.DeleteAsync(uri);

            return response.IsSuccessStatusCode ?
                new StatusResponse(StatusCode.Success) :
                new StatusResponse(StatusCode.Error, response.StatusCode.ToString());
        }

        #endregion

    }
}