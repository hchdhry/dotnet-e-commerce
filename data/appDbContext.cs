using Microsoft.EntityFrameworkCore;
using MVC_ecom.Model;

namespace MVC_ecom.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options){

        }
        public DbSet<Category>Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Category>().HasData(
            new Category{Id=1, Name = "Action", DisplayOrderNumber = 1},
            new Category {Id = 2, Name = "sciFi", DisplayOrderNumber = 2},
            new Category { Id = 3, Name = "History", DisplayOrderNumber = 3 }

            );
        }
    }
}
