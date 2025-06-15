using Microsoft.EntityFrameworkCore;
using MiraGames.Server.Entities;

namespace MiraGames.Server.Database.Contexts
{
    public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<Payment> Payments { get; private set; }
        public DbSet<Rate> Rates { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Payments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}
