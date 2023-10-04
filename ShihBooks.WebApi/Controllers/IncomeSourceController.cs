using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;
using System.Xml.Linq;

namespace ShihBooks.WebApi.Controllers
{
    [ApiController]
    [Route("api/source")]
    public class IncomeSourceController : ControllerBase
    {
        private readonly ILogger<IncomeSourceController> _logger;

        public IncomeSourceController(ILogger<IncomeSourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<IncomeSource>> GetSources(ApplicationDbContext db)
        {
            return await db.IncomeSources.OrderBy(e => e.Name).ToListAsync();
        }        

        [HttpPost]
        public async Task AddSource(ApplicationDbContext db, string name)
        {
            var t = await db.IncomeSources.FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower());
            if (t != null)
            {
                if (t.Name != name)
                {
                    await UpdateSource(db, t.Id, name);
                }
                return;
            }

            db.IncomeSources.Add(new IncomeSource { Name = name });
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateSource(ApplicationDbContext db, int id, string name)
        {
            var ev = await db.IncomeSources.FindAsync(id);
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
        public async Task DeleteSource(ApplicationDbContext db, int id)
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
