using Microsoft.EntityFrameworkCore;
using Ramayan_gita_app.Models;

namespace Ramayan_gita_app.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            base.OnModelCreating(modelBuilder);
        }
    }
}