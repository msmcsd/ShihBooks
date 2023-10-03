using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;

namespace ShihBooks.WebApi.Controllers
{
    [ApiController]
    [Route("api/incomesource")]
    public class IncomeSourceController : ControllerBase
    {
        private readonly ILogger<IncomeSourceController> _logger;

        public IncomeSourceController(ILogger<IncomeSourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<IncomeSource>> GetTypes(ApplicationDbContext db)
        {
            return await db.IncomeSources.ToListAsync();
        }

        [HttpPost]
        public async Task AddType(ApplicationDbContext db, IncomeSource incomeSource)
        {
            var t = await db.IncomeSources.FirstOrDefaultAsync(e => e.Name.Equals(incomeSource.Name, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                if (t.Name != incomeSource.Name)
                {
                    await UpdateType(db, incomeSource.Id, incomeSource.Name);
                }
                return;
            }

            db.IncomeSources.Add(incomeSource);
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateType(ApplicationDbContext db, int id, string newTypeName)
        {
            var ev = await db.IncomeSources.FindAsync(id);
            if (ev != null)
            {
                ev.Name = newTypeName;
                await db.SaveChangesAsync();
            }
        }

        [HttpDelete]
        public async Task DeleteType(ApplicationDbContext db, int id)
        {
            var e = await db.IncomeSources.FirstOrDefaultAsync(e => e.Id == id);
            if (e != null)
            {
                return;
            }

            var ev = await db.IncomeSources.FindAsync(id);
            if (ev != null)
            {
                db.IncomeSources.Remove(ev);
                await db.SaveChangesAsync();
            }
        }
    }

}
