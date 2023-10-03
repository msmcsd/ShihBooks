using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;

namespace ShihBooks.WebApi.Controllers
{
    [ApiController]
    [Route("api/type")]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly ILogger<ExpenseTypeController> _logger;

        public ExpenseTypeController(ILogger<ExpenseTypeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseType>> GetTypes(ApplicationDbContext db)
        {
            return await db.ExpenseTypes.ToListAsync();
        }

        [HttpPost]
        public async Task AddType(ApplicationDbContext db, ExpenseType expenseType)
        {
            var t = await db.ExpenseTypes.FirstOrDefaultAsync(e => e.Name.Equals(expenseType.Name, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                if (t.Name != expenseType.Name)
                {
                    await UpdateType(db, expenseType.Id, expenseType.Name);
                }
                return;
            }

            db.ExpenseTypes.Add(expenseType);
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateType(ApplicationDbContext db, int id, string newTypeName)
        {
            var ev = await db.ExpenseTypes.FindAsync(id);
            if (ev != null)
            {
                if (ev.Name != newTypeName)
                {
                    ev.Name = newTypeName;
                    await db.SaveChangesAsync();
                }
            }
        }

        [HttpDelete]
        public async Task DeleteType(ApplicationDbContext db, int id)
        {
            var e = await db.Expenses.FirstOrDefaultAsync(e => e.ExpenseTypeId == id);
            if (e != null)
            {
                return;
            }

            var ev = await db.ExpenseTypes.FindAsync(id);
            if (ev != null)
            {
                db.ExpenseTypes.Remove(ev);
                await db.SaveChangesAsync();
            }
        }
    }

}
