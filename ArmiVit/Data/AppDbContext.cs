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
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<ServiceProgramItem> ServiceProgramItems { get; set; }

    }
}
