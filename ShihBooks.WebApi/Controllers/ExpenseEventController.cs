using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;

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
            return await db.ExpenseEvents.ToListAsync();
        }       
        
        [HttpPost]
        public async Task AddEvent(ApplicationDbContext db, ExpenseEvent expenseEvent)
        {
            var t = await db.ExpenseEvents.FirstOrDefaultAsync(e => e.Name.Equals(expenseEvent.Name, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                if (t.Name != expenseEvent.Name)
                {
                    await UpdateEvent(db, expenseEvent.Id, expenseEvent.Name);
                }
                return;
            }

            db.ExpenseEvents.Add(expenseEvent);
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateEvent(ApplicationDbContext db, int id, string newEventName)
        {
            var ev = await db.ExpenseEvents.FindAsync(id);
            if (ev != null)
            {
                ev.Name = newEventName;
                await db.SaveChangesAsync();
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
