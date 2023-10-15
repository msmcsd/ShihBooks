using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.UseCases.IncomeSources;

namespace ShihBooks.UnitTests.UseCases
{
    [TestFixture]
    public class IncomeSourceTests
    {
        private InMemoryExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetIncomeSources_Returns_Expected_Count()
        {
            var useCase = new ViewIncomeSourcesUseCase(_dataStore);
            var IncomeSources = await useCase.ExecuteAsync();

            Assert.That(IncomeSources.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Add_IncomeSource_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new AddIncomeSourceUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Add_IncomeSource_With_Valid_Name_Returns_Success()
        {
            var useCase = new AddIncomeSourceUseCase(_dataStore);
            var newIncomeSource = "abc";
            var response = await useCase.ExecuteAsync(newIncomeSource);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var incomeSources = await new ViewIncomeSourcesUseCase(_dataStore).ExecuteAsync();
            var e = incomeSources.FirstOrDefault(e => e.Name.ToLower() == newIncomeSource.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Add_IncomeSource_With_Existing_Name_Returns_EntityExists()
        {
            var useCase = new AddIncomeSourceUseCase(_dataStore);
            var newIncomeSource = "Interest";
            var response = await useCase.ExecuteAsync(newIncomeSource);
            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityExists));

            var IncomeSources = await new ViewIncomeSourcesUseCase(_dataStore).ExecuteAsync();
            Assert.That(IncomeSources.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Add_IncomeSource_With_Valid_Name_And_Url_Returns_Success()
        {
            var useCase = new AddIncomeSourceUseCase(_dataStore);
            var newIncomeSource = "abc";
            var url = "def";
            var response = await useCase.ExecuteAsync(newIncomeSource, url);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var incomeSources = await new ViewIncomeSourcesUseCase(_dataStore).ExecuteAsync();
            Assert.That(incomeSources.Count, Is.EqualTo(4));

            var incomeSource = incomeSources.FirstOrDefault(e => e.Name.ToLower() == newIncomeSource.ToLower() && e.ImageUrl.ToLower() == url.ToLower());
            Assert.That(incomeSource, Is.Not.Null);
        }

        [Test]
        public async Task Update_IncomeSource_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new UpdateIncomeSourceUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(0, null, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Update_IncomeSource_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new UpdateIncomeSourceUseCase(_dataStore);

            var IncomeSourceId = -1;
            var newIncomeSource = "abc";
            var response = await useCase.ExecuteAsync(IncomeSourceId, newIncomeSource, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Update_IncomeSource_With_Valid_Name_Returns_Success()
        {
            var useCase = new UpdateIncomeSourceUseCase(_dataStore);

            var IncomeSourceId = 1;
            var newIncomeSource = "abc";
            var response = await useCase.ExecuteAsync(IncomeSourceId, newIncomeSource, null);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var IncomeSources = await new ViewIncomeSourcesUseCase(_dataStore).ExecuteAsync();
            var e = IncomeSources.FirstOrDefault(e => e.Name.ToLower() == newIncomeSource.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Delete_IncomeSource_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new DeleteIncomeSourceUseCase(_dataStore);

            var IncomeSourceId = -1;
            var response = await useCase.ExecuteAsync(IncomeSourceId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Delete_IncomeSource_With_Id_Used_By_Income_Returns_EntityInUse()
        {
            var useCase = new DeleteIncomeSourceUseCase(_dataStore);

            var IncomeSourceId = 1;
            var response = await useCase.ExecuteAsync(IncomeSourceId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityInUse));
        }

        [Test]
        public async Task Delete_IncomeSource_With_Id_Not_Used_By_Income_Returns_Success()
        {
            var useCase = new DeleteIncomeSourceUseCase(_dataStore);

            var IncomeSourceId = 3;
            var response = await useCase.ExecuteAsync(IncomeSourceId);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
    }
}
