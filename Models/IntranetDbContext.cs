using Intranet_API.Models.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Intranet_API.Models
{
    public class IntranetDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public IntranetDbContext(DbContextOptions<IntranetDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>()
                .HasOne(e => e.RefreshToken)
                .WithOne(e => e.User)
                .HasForeignKey<RefreshToken>(e => e.UserId)
                .IsRequired();

            model.Entity<RefreshToken>(entity => {
                entity.HasKey(x => x.Id);
            });

        }
    }
}
