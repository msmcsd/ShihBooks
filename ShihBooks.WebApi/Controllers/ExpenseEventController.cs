using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;
using System.Xml.Linq;

namespace ShihBooks.WebApi.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class ExpenseEventController : ControllerBase
    {
        private readonly ILogger<ExpenseEventController> _logger;

        public ExpenseEventController(ILogger<ExpenseEventController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseEvent>> GetEvents(ApplicationDbContext db)
        {
            return await db.ExpenseEvents.OrderBy(e => e.Name).ToListAsync();
        }       
        
        [HttpPost]
        public async Task AddEvent(ApplicationDbContext db, string name)
        {
            var t = await db.ExpenseEvents.FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower());
            if (t != null)
            {
                if (t.Name != name)
                {
                    await UpdateEvent(db, t.Id, name);
                }
                return;
            }

            db.ExpenseEvents.Add(new ExpenseEvent { Name = name });
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateEvent(ApplicationDbContext db, int id, string name)
        {
            var ev = await db.ExpenseEvents.FindAsync(id);
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
        public async Task DeleteEvent(ApplicationDbContext db, int id)
        {
            var e = await db.Expenses.FirstOrDefaultAsync(e => e.EventId == id);
            if (e != null)
            {
                return;
            }

            var ev = await db.ExpenseEvents.FindAsync(id);
            if (ev != null)
            {
                db.ExpenseEvents.Remove(ev);
                await db.SaveChangesAsync();
            }
        }
    }

}
