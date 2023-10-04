namespace ShihBooks.Plugins.DataStore.WebApi
{
    public class Constants
    {
        //public const string WebApiBaseUrl = "https://10.0.2.2:7091/api";
        //public const string WebApiBaseUrl = "https://192.168.1.158:7091/api";
        public const string WebApiBaseUrl = "https://localhost:7091/api";

        public const string WebApiExpenseTagUrl = $"{WebApiBaseUrl}/tag";
        public const string WebApiExpenseEventUrl = $"{WebApiBaseUrl}/event";
        public const string WebApiExpenseTypeUrl = $"{WebApiBaseUrl}/type";
        public const string WebApiIncomeSourceUrl = $"{WebApiBaseUrl}/source";
        public const string WebApiMerchantUrl = $"{WebApiBaseUrl}/merchant";
    }
}
