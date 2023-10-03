using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;

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
            return await db.Merchants.ToListAsync();
        }

        [HttpPost]
        public async Task AddMerchant(ApplicationDbContext db, Merchant merchant)
        {
            var t = await db.Merchants.FirstOrDefaultAsync(e => e.Name.Equals(merchant.Name, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                if (t.Name != merchant.Name)
                {
                    await UpdateMerchant(db, merchant.Id, merchant.Name);
                }
                return;
            }

            db.Merchants.Add(merchant);
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateMerchant(ApplicationDbContext db, int id, string newMerchantName)
        {
            var ev = await db.Merchants.FindAsync(id);
            if (ev != null)
            {
                ev.Name = newMerchantName;
                if (ev.Name != newMerchantName)
                {
                    ev.Name = newMerchantName;
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
