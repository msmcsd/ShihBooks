using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;
using System.Xml.Linq;

namespace ShihBooks.WebApi.Controllers
{
    [ApiController]
    [Route("api/merchant")]
    public class MerchantController : ControllerBase
    {
        private readonly ILogger<MerchantController> _logger;

        public MerchantController(ILogger<MerchantController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Merchant>> GetMerchants(ApplicationDbContext db)
        {
            return await db.Merchants.OrderBy(e => e.Name).ToListAsync();
        }

        [HttpPost]
        public async Task AddMerchant(ApplicationDbContext db, string name)
        {
            var t = await db.Merchants.FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower());
            if (t != null)
            {
                if (t.Name != name)
                {
                    await UpdateMerchant(db, t.Id, name);
                }
                return;
            }

            db.Merchants.Add(new Merchant { Name = name });
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateMerchant(ApplicationDbContext db, int id, string name)
        {
            var ev = await db.Merchants.FindAsync(id);
            if (ev != null)
            {
                ev.Name = name;
                if (ev.Name != name)
                {
                    ev.Name = name;
                    await db.SaveChangesAsync();
                }
            }
        }

        [HttpDelete]
        public async Task DeleteMerchant(ApplicationDbContext db, int id)
        {
            var e = await db.Expenses.FirstOrDefaultAsync(e => e.MerchantId == id);
            if (e != null)
            {
                return;
            }

            var ev = await db.Merchants.FindAsync(id);
            if (ev != null)
            {
                db.Merchants.Remove(ev);
                await db.SaveChangesAsync();
            }
        }
    }

}
