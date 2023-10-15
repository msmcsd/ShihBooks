using ShihBooks.Core.StatusResponses;
using ShihBooks.Plugins.DataStore.InMemory;
using ShihBooks.UseCases.PluginInterfaces;
using ShihBooks.UseCases.UseCases.Incomes;
using ShihBooks.Core.Incomes;

namespace ShihBooks.UnitTests.UseCases
{
    [TestFixture]
    public class IncomeTests
    {
        private IExpensesDataStore _dataStore { get; set; }

        [SetUp]
        public void Setup()
        {
            _dataStore = new InMemoryExpensesDataStore();
        }

        [Test]
        public async Task GetIncomes_Returns_Expected_Count()
        {
            var useCase = new ViewIncomesByMonthUseCase(_dataStore);
            var incomes = await useCase.ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);

            Assert.That(incomes, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task Add_Income_With_Invalid_Income_Returns_InvalidEntity()
        {
            var useCase = new AddIncomeUseCase(_dataStore);
            var response = await useCase.ExecuteAsync(null);

            Assert.That(response.Status, Is.EqualTo(StatusCode.InvalidEntity));
        }

        [Test]
        public async Task Add_Income_Returns_Success()
        {
            var useCase = new AddIncomeUseCase(_dataStore);
            var newIncome = new Income
            {
                Description = "Test",
                Amount = 123,
                Note = "new",
                RecipientId = 2,
                IncomeDate = DateTime.Now,
                SourceId = 2
            };
            var response = await useCase.ExecuteAsync(newIncome);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var incomes = await new ViewIncomesByMonthUseCase(_dataStore).ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);
            Assert.That(incomes, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task Update_Income_With_Non_Existing_Id_Returns_IncomeNotFound()
        {
            var useCase = new UpdateIncomeUseCase(_dataStore);
            var newIncome = new Income
            {
                Id = -1,
            };
            var response = await useCase.ExecuteAsync(newIncome);
            Assert.That(response.Status, Is.EqualTo(StatusCode.IncomeNotFound));

            var incomes = await new ViewIncomesByMonthUseCase(_dataStore).ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);
            Assert.That(incomes, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task Update_Income_With_Existing_Id_Returns_Success()
        {
            var useCase = new UpdateIncomeUseCase(_dataStore);
            var newIncome = new Income
            {
                Id = 1,
                Description = "Test",
                Amount = 123,
                Note = "new",
                RecipientId = 2,
                IncomeDate = DateTime.Now,
                SourceId = 1
            };
            var response = await useCase.ExecuteAsync(newIncome);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var incomes = await new ViewIncomesByMonthUseCase(_dataStore).ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);
            var income = incomes.FirstOrDefault(e => e.Id == newIncome.Id);
            Assert.That(income, Is.Not.Null);
            Assert.That(income.Description, Is.EqualTo(newIncome.Description));
            Assert.That(income.Amount, Is.EqualTo(newIncome.Amount));
            Assert.That(income.Note, Is.EqualTo(newIncome.Note));
            Assert.That(income.RecipientId, Is.EqualTo(newIncome.RecipientId));
            Assert.That(income.SourceId, Is.EqualTo(newIncome.SourceId));
            Assert.That(income.IncomeDate, Is.EqualTo(newIncome.IncomeDate));
        }

        [Test]
        public async Task Delete_Income_With_Non_Existing_Id_Returns_IncomeNotFound()
        {
            var useCase = new DeleteIncomeUseCase(_dataStore);

            var incomeId = -1;
            var response = await useCase.ExecuteAsync(incomeId);

            Assert.That(response.Status, Is.EqualTo(StatusCode.IncomeNotFound));
        }

        [Test]
        public async Task Delete_Income_With_Existing_Id_Used_Returns_Success()
        {
            var useCase = new DeleteIncomeUseCase(_dataStore);

            var incomeId = 2;
            var response = await useCase.ExecuteAsync(incomeId);
            Assert.That(response.IsSuccess, Is.EqualTo(true));

            var incomes = await new ViewIncomesByMonthUseCase(_dataStore).ExecuteAsync(DateTime.Now.Year, DateTime.Now.Month);
            Assert.That(incomes, Has.Count.EqualTo(1));
        }
    }
}
