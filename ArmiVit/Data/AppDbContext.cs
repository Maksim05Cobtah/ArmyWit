using ArmiVit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<AboutPage> AboutPages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<GetIn> GetIns { get; set; }
    }
}

