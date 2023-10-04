using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;

namespace ShihBooks.WebApi.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class ExpenseTagController : ControllerBase
    {
        private readonly ILogger<ExpenseTagController> _logger;

        public ExpenseTagController(ILogger<ExpenseTagController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseTag>> GetTags(ApplicationDbContext db)
        {
            return await db.ExpenseTags.OrderBy(e => e.Name).ToListAsync();
        }

        [HttpPost]
        public async Task AddTag(ApplicationDbContext db, string name)
        {
            var t = await db.ExpenseTags.FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower());
            if (t != null)
            {
                if (t.Name != name)
                {
                    await UpdateTag(db, t.Id, name);
                }
                return;
            }

            db.ExpenseTags.Add(new ExpenseTag { Name = name});
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateTag(ApplicationDbContext db, int id, string name)
        {
            var ev = await db.ExpenseTags.FindAsync(id);
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
        public async Task DeleteTag(ApplicationDbContext db, int id)
        {
            var e = await db.Expenses.FirstOrDefaultAsync(e => e.TagId == id);
            if (e != null)
            {
                return;
            }

            var ev = await db.ExpenseTags.FindAsync(id);
            if (ev != null)
            {
                db.ExpenseTags.Remove(ev);
                await db.SaveChangesAsync();
            }
        }
    }

}
