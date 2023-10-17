using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.UseCases.Merchants;

namespace ShihBooks.UnitTests.UseCases
{
    [TestFixture]
    public class MerchantTests
    {
        private InMemoryExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetMerchants_Returns_Expected_Count()
        {
            var useCase = new ViewMerchantsUseCase(_dataStore);
            var merchants = await useCase.ExecuteAsync();

            Assert.That(merchants.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Add_Merchant_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new AddMerchantUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Add_Merchant_With_Valid_Name_Returns_Success()
        {
            var useCase = new AddMerchantUseCase(_dataStore);
            var newMerchant = "abc";
            var response = await useCase.ExecuteAsync(newMerchant);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var merchants = await new ViewMerchantsUseCase(_dataStore).ExecuteAsync();
            var e = merchants.FirstOrDefault(e => e.Name.ToLower() == newMerchant.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Add_Merchant_With_Existing_Name_Returns_EntityExists()
        {
            var useCase = new AddMerchantUseCase(_dataStore);
            var newMerchant = "Costco";
            var response = await useCase.ExecuteAsync(newMerchant);
            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityExists));

            var merchants = await new ViewMerchantsUseCase(_dataStore).ExecuteAsync();
            Assert.That(merchants.Count, Is.EqualTo(3));
        }
        
        [Test]
        public async Task Add_Merchant_With_Valid_Name_And_Url_Returns_Success()
        {
            var useCase = new AddMerchantUseCase(_dataStore);
            var newMerchant = "abc";
            var url = "def";
            var response = await useCase.ExecuteAsync(newMerchant, url);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var merchants = await new ViewMerchantsUseCase(_dataStore).ExecuteAsync();
            Assert.That(merchants.Count, Is.EqualTo(4));

            var merchant = merchants.FirstOrDefault(e => e.Name.ToLower() == newMerchant.ToLower() && e.ImageUrl.ToLower() == url.ToLower());
            Assert.That(merchant, Is.Not.Null);
        }

        [Test]
        public async Task Update_Merchant_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new UpdateMerchantUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(0, null, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Update_Merchant_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new UpdateMerchantUseCase(_dataStore);

            var merchantId = -1;
            var newMerchant = "abc";
            var response = await useCase.ExecuteAsync(merchantId, newMerchant, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Update_Merchant_With_Valid_Name_Returns_Success()
        {
            var useCase = new UpdateMerchantUseCase(_dataStore);

            var merchantId = 1;
            var newMerchant = "abc";
            var response = await useCase.ExecuteAsync(merchantId, newMerchant, null);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var merchants = await new ViewMerchantsUseCase(_dataStore).ExecuteAsync();
            var e = merchants.FirstOrDefault(e => e.Name.ToLower() == newMerchant.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Delete_Merchant_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new DeleteMerchantUseCase(_dataStore);

            var merchantId = -1;
            var response = await useCase.ExecuteAsync(merchantId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Delete_Merchant_With_Id_Used_By_Expense_Returns_EntityInUse()
        {
            var useCase = new DeleteMerchantUseCase(_dataStore);

            var merchantId = 1;
            var response = await useCase.ExecuteAsync(merchantId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityInUse));
        }

        [Test]
        public async Task Delete_Merchant_With_Id_Not_Used_By_Expense_Returns_Success()
        {
            var useCase = new DeleteMerchantUseCase(_dataStore);

            var merchantId = 3;
            var response = await useCase.ExecuteAsync(merchantId);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
    }
}
