using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.UseCases.ExpenseTags;

namespace ShihBooks.UnitTests.UseCases
{
    [TestFixture]
    public class ExpenseTagTests
    {
        private InMemoryExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetTags_Returns_Expected_Count()
        {
            var useCase = new ViewExpenseTagsUseCase(_dataStore);
            var tags = await useCase.ExecuteAsync();

            Assert.That(tags.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Add_Tag_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new AddExpenseTagUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Add_Tag_With_Valid_Name_Returns_Success()
        {
            var useCase = new AddExpenseTagUseCase(_dataStore);
            var newTag = "abc";
            var response = await useCase.ExecuteAsync(newTag);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
            var tags = await new ViewExpenseTagsUseCase(_dataStore).ExecuteAsync();
            var e = tags.FirstOrDefault(e => e.Name.ToLower() == newTag.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Add_Tag_With_Existing_Name_Returns_EntityExists()
        {
            var useCase = new AddExpenseTagUseCase(_dataStore);
            var newTag = "Kids";
            var response = await useCase.ExecuteAsync(newTag);
            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityExists));

            var tags = await new ViewExpenseTagsUseCase(_dataStore).ExecuteAsync();
            Assert.That(tags.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Update_Tag_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new UpdateExpenseTagUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(0, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Update_Tag_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new UpdateExpenseTagUseCase(_dataStore);

            var tagId = -1;
            var newTag = "abc";
            var response = await useCase.ExecuteAsync(tagId, newTag);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Update_Tag_With_Valid_Name_Returns_Success()
        {
            var useCase = new UpdateExpenseTagUseCase(_dataStore);

            var tagId = 1;
            var newTag = "abc";
            var response = await useCase.ExecuteAsync(tagId, newTag);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
            var tags = await new ViewExpenseTagsUseCase(_dataStore).ExecuteAsync();
            var e = tags.FirstOrDefault(e => e.Name.ToLower() == newTag.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Delete_Tag_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new DeleteExpenseTagUseCase(_dataStore);

            var tagId = -1;
            var response = await useCase.ExecuteAsync(tagId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Delete_Tag_With_Id_Used_By_Expense_Returns_EntityInUse()
        {
            var useCase = new DeleteExpenseTagUseCase(_dataStore);

            var tagId = 1;
            var response = await useCase.ExecuteAsync(tagId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityInUse));
        }

        [Test]
        public async Task Delete_Tag_With_Id_Not_Used_By_Expense_Returns_Success()
        {
            var useCase = new DeleteExpenseTagUseCase(_dataStore);

            var tagId = 3;
            var response = await useCase.ExecuteAsync(tagId);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
    }
}
