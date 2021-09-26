using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Application;

namespace Portfolio_Management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddModels();
            base.OnModelCreating(modelBuilder);
        }
    }
}