using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;

namespace ShihBooks.WebApi.Controllers
{
    [ApiController]
    [Route("api/expense")]
    public class ExpenseController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ExpenseView>> GetExpenses(ApplicationDbContext db, int year, int month)
        {
            var expenses = (from e in db.Expenses
                            join m in db.Merchants
                            on e.MerchantId equals m.Id
                            where e.ExpenseDate.Year == year && e.ExpenseDate.Month == month
                            select new ExpenseView
                            {
                                Id = e.Id,
                                Description = e.Description,
                                Amount = e.Amount,
                                ExpenseDate = e.ExpenseDate,
                                MerchantId = e.MerchantId,
                                ExpenseTypeId = e.ExpenseTypeId,
                                MerchantImageUrl = m.ImageUrl,
                                TagId = e.TagId,
                                EventId = e.EventId,
                                Note = e.Note
                            }).ToList();

            return expenses;
        }

        [HttpPost]
        public async Task AddExpense(ApplicationDbContext db, Expense expense)
        {
            if (expense == null) return;

            db.Expenses.Add(expense);
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateEvent(ApplicationDbContext db, Expense expense)
        {
            var ev = await db.Expenses.FindAsync(expense.Id);
            if (ev != null)
            {
                ev.Description = expense.Description;
                ev.Amount = expense.Amount;
                ev.EventId = expense.EventId;
                ev.Note = expense.Note;
                ev.ExpenseDate = expense.ExpenseDate;
                ev.MerchantId = expense.MerchantId;
                ev.ExpenseTypeId = expense.ExpenseTypeId;
                ev.TagId = expense.TagId;

                await db.SaveChangesAsync();
            }
        }

        [HttpDelete]
        public async Task DeleteExpense(ApplicationDbContext db, int id)
        {
            var e = await db.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (e == null)
            {
                return;
            }

            db.Expenses.Remove(e);
            await db.SaveChangesAsync();
        }
    }
}
