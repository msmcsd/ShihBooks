using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.UseCases.ExpenseTypes;

namespace ShihBooks.UnitTests.UseCases
{
    [TestFixture]
    public class ExpenseTypeTests
    {
        private InMemoryExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetTypes_Returns_Expected_Count()
        {
            var useCase = new ViewExpenseTypesUseCase(_dataStore);
            var types = await useCase.ExecuteAsync();

            Assert.That(types.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Add_Type_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new AddExpenseTypeUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Add_Type_With_Valid_Name_Returns_Success()
        {
            var useCase = new AddExpenseTypeUseCase(_dataStore);
            var newType = "abc";
            var response = await useCase.ExecuteAsync(newType);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
            var types = await new ViewExpenseTypesUseCase(_dataStore).ExecuteAsync();
            var e = types.FirstOrDefault(e => e.Name.ToLower() == newType.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Add_Type_With_Existing_Name_Returns_EntityExists()
        {
            var useCase = new AddExpenseTypeUseCase(_dataStore);
            var newType = "Grocery";
            var response = await useCase.ExecuteAsync(newType);
            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityExists));

            var types = await new ViewExpenseTypesUseCase(_dataStore).ExecuteAsync();
            Assert.That(types.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Update_Type_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new UpdateExpenseTypeUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(0, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Update_Type_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new UpdateExpenseTypeUseCase(_dataStore);

            var tagId = -1;
            var newType = "abc";
            var response = await useCase.ExecuteAsync(tagId, newType);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Update_Type_With_Valid_Name_Returns_Success()
        {
            var useCase = new UpdateExpenseTypeUseCase(_dataStore);

            var tagId = 1;
            var newType = "abc";
            var response = await useCase.ExecuteAsync(tagId, newType);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
            var types = await new ViewExpenseTypesUseCase(_dataStore).ExecuteAsync();
            var e = types.FirstOrDefault(e => e.Name.ToLower() == newType.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Delete_Type_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new DeleteExpenseTypeUseCase(_dataStore);

            var tagId = -1;
            var response = await useCase.ExecuteAsync(tagId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Delete_Type_With_Id_Used_By_Expense_Returns_EntityInUse()
        {
            var useCase = new DeleteExpenseTypeUseCase(_dataStore);

            var tagId = 1;
            var response = await useCase.ExecuteAsync(tagId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityInUse));
        }

        [Test]
        public async Task Delete_Type_With_Id_Not_Used_By_Expense_Returns_Success()
        {
            var useCase = new DeleteExpenseTypeUseCase(_dataStore);

            var tagId = 3;
            var response = await useCase.ExecuteAsync(tagId);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
    }
}
