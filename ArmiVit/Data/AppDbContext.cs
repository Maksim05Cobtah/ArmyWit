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
        public DbSet<Service> Services { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<ServiceProgramItem> ServiceProgramItems { get; set; }
        public DbSet<CustomSection> CustomSections { get; set; }
        public DbSet<PageElement> PageElements { get; set; }
        public DbSet<AboutContent> AboutContents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Обов'язково викликаємо base для налаштування таблиць Identity (користувачі, ролі тощо)
            base.OnModelCreating(builder);

            // 1. Точність для суми (decimal) у TrainingProgram
            builder.Entity<TrainingProgram>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);

            // 2. Зв'язок CustomSection -> PageElements (Каскадне видалення)
            builder.Entity<PageElement>()
                .HasOne(e => e.CustomSection)
                .WithMany(s => s.Elements)
                .HasForeignKey(e => e.CustomSectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Зв'язок TrainingProgram -> ServiceProgramItems (Каскадне видалення)
            builder.Entity<ServiceProgramItem>()
                .HasOne(i => i.TrainingProgram)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.TrainingProgramId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}