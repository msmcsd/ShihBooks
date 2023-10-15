using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.UseCases.ExpenseEvents;

namespace ShihBooks.UnitTests.UseCases
{
    public class ExpenseEventTests
    {
        private InMemoryExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetEvents_Returns_Expected_Count()
        {
            var useCase = new ViewExpenseEventsUseCase(_dataStore);
            var events = await useCase.ExecuteAsync();

            Assert.That(events.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Add_Event_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new AddExpenseEventUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Add_Event_With_Valid_Name_Returns_Success()
        {
            var useCase = new AddExpenseEventUseCase(_dataStore);
            var newEvent = "abc";
            var response = await useCase.ExecuteAsync(newEvent);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
            var events = await new ViewExpenseEventsUseCase(_dataStore).ExecuteAsync();
            var e = events.FirstOrDefault(e => e.Name.ToLower() == newEvent.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Add_Event_With_Existing_Name_Returns_EntityExists()
        {
            var useCase = new AddExpenseEventUseCase(_dataStore);
            var newEvent = "Travel";
            var response = await useCase.ExecuteAsync(newEvent);
            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityExists));

            var events = await new ViewExpenseEventsUseCase(_dataStore).ExecuteAsync();
            Assert.That(events.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Update_Event_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new UpdateExpenseEventUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(0, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Update_Event_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new UpdateExpenseEventUseCase(_dataStore);

            var eventId = -1;
            var newEvent = "abc";
            var response = await useCase.ExecuteAsync(eventId, newEvent);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Update_Event_With_Valid_Name_Returns_Success()
        {
            var useCase = new UpdateExpenseEventUseCase(_dataStore);

            var eventId = 1;
            var newEvent = "abc";
            var response = await useCase.ExecuteAsync(eventId, newEvent);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
            var events = await new ViewExpenseEventsUseCase(_dataStore).ExecuteAsync();
            var e = events.FirstOrDefault(e => e.Name.ToLower() == newEvent.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Delete_Event_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new DeleteExpenseEventUseCase(_dataStore);

            var eventId = -1;
            var response = await useCase.ExecuteAsync(eventId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Delete_Event_With_Id_Used_By_Expense_Returns_EntityInUse()
        {
            var useCase = new DeleteExpenseEventUseCase(_dataStore);

            var eventId = 2;
            var response = await useCase.ExecuteAsync(eventId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityInUse));
        }

        [Test]
        public async Task Delete_Event_With_Id_Not_Used_By_Expense_Returns_Success()
        {
            var useCase = new DeleteExpenseEventUseCase(_dataStore);

            var eventId = 3;
            var response = await useCase.ExecuteAsync(eventId);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
    }
}