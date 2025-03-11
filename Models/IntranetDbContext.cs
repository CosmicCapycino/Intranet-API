using Intranet_API.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Intranet_API.Models
{
    public class IntranetDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public IntranetDbContext(DbContextOptions<IntranetDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>().HasKey(x => x.Id);
        }
    }
}
