using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.UseCases.IncomeRecipients;

namespace ShihBooks.UnitTests.UseCases
{
    [TestFixture]
    public class IncomeRecipientTests
    {
        private InMemoryExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetRecipients_Returns_Expected_Count()
        {
            var useCase = new ViewIncomeRecipientsUseCase(_dataStore);
            var recipients = await useCase.ExecuteAsync();

            Assert.That(recipients.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Add_Recipient_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new AddIncomeRecipientUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Add_Recipient_With_Valid_Name_Returns_Success()
        {
            var useCase = new AddIncomeRecipientUseCase(_dataStore);
            var newRecipient = "abc";
            var response = await useCase.ExecuteAsync(newRecipient);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var recipients = await new ViewIncomeRecipientsUseCase(_dataStore).ExecuteAsync();
            var e = recipients.FirstOrDefault(e => e.Name.ToLower() == newRecipient.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Add_Recipient_With_Existing_Name_Returns_EntityExists()
        {
            var useCase = new AddIncomeRecipientUseCase(_dataStore);
            var newRecipient = "John1";
            var response = await useCase.ExecuteAsync(newRecipient);
            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityExists));

            var recipients = await new ViewIncomeRecipientsUseCase(_dataStore).ExecuteAsync();
            Assert.That(recipients.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task Update_Recipient_With_Invalid_Name_Returns_InvalidEntityName()
        {
            var useCase = new UpdateIncomeRecipientUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(0, null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntityName));
        }

        [Test]
        public async Task Update_Recipient_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new UpdateIncomeRecipientUseCase(_dataStore);

            var recipientId = -1;
            var newRecipient = "abc";
            var response = await useCase.ExecuteAsync(recipientId, newRecipient);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Update_Recipient_With_Valid_Name_Returns_Success()
        {
            var useCase = new UpdateIncomeRecipientUseCase(_dataStore);

            var recipientId = 1;
            var newRecipient = "abc";
            var response = await useCase.ExecuteAsync(recipientId, newRecipient);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
            var recipients = await new ViewIncomeRecipientsUseCase(_dataStore).ExecuteAsync();
            var e = recipients.FirstOrDefault(e => e.Name.ToLower() == newRecipient.ToLower());
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public async Task Delete_Recipient_With_Non_Existing_Id_Returns_EntityNotFound()
        {
            var useCase = new DeleteIncomeRecipientUseCase(_dataStore);

            var recipientId = -1;
            var response = await useCase.ExecuteAsync(recipientId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityNotFound));
        }

        [Test]
        public async Task Delete_Recipient_With_Id_Used_By_Expense_Returns_EntityInUse()
        {
            var useCase = new DeleteIncomeRecipientUseCase(_dataStore);

            var recipientId = 2;
            var response = await useCase.ExecuteAsync(recipientId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.EntityInUse));
        }

        [Test]
        public async Task Delete_Recipient_With_Id_Not_Used_By_Expense_Returns_Success()
        {
            var useCase = new DeleteIncomeRecipientUseCase(_dataStore);

            var recipientId = 3;
            var response = await useCase.ExecuteAsync(recipientId);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }
    }
}
