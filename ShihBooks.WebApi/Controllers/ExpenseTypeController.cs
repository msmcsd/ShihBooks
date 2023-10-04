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
            return await db.ExpenseTypes.OrderBy(e => e.Name).ToListAsync();
        }

        [HttpPost]
        public async Task AddType(ApplicationDbContext db, string name)
        {
            var t = await db.ExpenseTypes.FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower());
            if (t != null)
            {
                if (t.Name != name)
                {
                    await UpdateType(db, t.Id, name);
                }
                return;
            }

            db.ExpenseTypes.Add(new ExpenseType { Name = name });
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateType(ApplicationDbContext db, int id, string name)
        {
            var ev = await db.ExpenseTypes.FindAsync(id);
            if (ev != null)
            {
                if (ev.Name != name)
                {
                    ev.Name = name;
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
