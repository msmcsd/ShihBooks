using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.UseCases.UseCases.Expenses;
using ShihBooks.Core.Expenses;

namespace ShihBooks.UnitTests.UseCases
{
    [TestFixture]
    public class ExpenseTests
    {
        private IExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetExpenses_Returns_Expected_Count()
        {
            var useCase = new ViewExpensesByMonthUseCase(_dataStore);
            var Expenses = await useCase.ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);

            Assert.That(Expenses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Add_Expense_With_Invalid_Expense_Returns_InvalidEntity()
        {
            var useCase = new AddExpenseUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntity));
        }

        [Test]
        public async Task Add_Expense_Returns_Success()
        {
            var useCase = new AddExpenseUseCase(_dataStore);
            var newExpense = new Expense
            {
                Description = "Test",
                Amount = 123,
                Note = "new",
                TagId = 2,
                EventId = 3,
                ExpenseTypeId = 2,
                ExpenseDate = DateTime.Now,
                MerchantId = 1
            };
            var response = await useCase.ExecuteAsync(newExpense);

            Assert.That(response.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public async Task Update_Expense_With_Non_Existing_Id_Returns_ExpenseNotFound()
        {
            var useCase = new UpdateExpenseUseCase(_dataStore);
            var newExpense = new Expense
            {
                Id = -1,
                Description = "Test",
                Amount = 123,
                Note = "new",
                TagId = 2,
                EventId = 3,
                ExpenseTypeId = 2,
                ExpenseDate = DateTime.Now,
                MerchantId = 1
            };
            var response = await useCase.ExecuteAsync(newExpense);
            Assert.That(response.Status, Is.EqualTo(StatusCode.ExpenseNotFound));

            var expenses = await new ViewExpensesByMonthUseCase(_dataStore).ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);
            Assert.That(expenses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Update_Expense_With_Existing_Id_Returns_Success()
        {
            var useCase = new UpdateExpenseUseCase(_dataStore);
            var newExpense = new Expense
            {
                Id = 1,
                Description = "Test",
                Amount = 123,
                Note = "new",
                TagId = 2,
                EventId = 3,
                ExpenseTypeId = 2,
                ExpenseDate = DateTime.Now,
                MerchantId = 1
            };
            var response = await useCase.ExecuteAsync(newExpense);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var expenses = await new ViewExpensesByMonthUseCase(_dataStore).ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);
            var expense = expenses.FirstOrDefault(e => e.Id == newExpense.Id);
            Assert.That(expense, Is.Not.Null);
            Assert.That(expense.Description, Is.EqualTo(newExpense.Description));
            Assert.That(expense.Amount, Is.EqualTo(newExpense.Amount));
            Assert.That(expense.Note, Is.EqualTo(newExpense.Note));
            Assert.That(expense.TagId, Is.EqualTo(newExpense.TagId));
            Assert.That(expense.EventId, Is.EqualTo(newExpense.EventId));
            Assert.That(expense.ExpenseTypeId, Is.EqualTo(newExpense.ExpenseTypeId));
            Assert.That(expense.ExpenseDate, Is.EqualTo(newExpense.ExpenseDate));
            Assert.That(expense.MerchantId, Is.EqualTo(newExpense.MerchantId));
        }

        [Test]
        public async Task Delete_Expense_With_Non_Existing_Id_Returns_ExpenseNotFound()
        {
            var useCase = new DeleteExpenseUseCase(_dataStore);

            var ExpenseId = -1;
            var response = await useCase.ExecuteAsync(ExpenseId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.ExpenseNotFound));
        }

        [Test]
        public async Task Delete_Expense_With_Existing_Id_Used_Returns_Success()
        {
            var useCase = new DeleteExpenseUseCase(_dataStore);

            var expenseId = 2;
            var response = await useCase.ExecuteAsync(expenseId);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var expenses = await new ViewExpensesByMonthUseCase(_dataStore).ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);
            Assert.That(expenses.Count, Is.EqualTo(1));
        }
    }
}
