using Microsoft.EntityFrameworkCore;
using ShihBooks.WebApi.Models;

namespace ShihBooks.WebApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<ExpenseTag> ExpenseTags { get; set; }

        public DbSet<ExpenseEvent> ExpenseEvents { get; set; }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        public DbSet<IncomeSource> IncomeSources { get; set; }

        public DbSet<Merchant> Merchants { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
