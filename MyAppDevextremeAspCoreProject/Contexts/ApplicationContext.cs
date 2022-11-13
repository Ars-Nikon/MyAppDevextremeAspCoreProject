using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Models;
using MyAppDevextremeAspCoreProject.Utilities;

namespace MyAppDevextremeAspCoreProject.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Filial> Filials { get; set; } = null!;
        public DbSet<ScheduleTime> ScheduleTimes { get; set; } = null!;
        public DbSet<Speciality> Specialities { get; set; } = null!;
        public DbSet<TimeTable> TimeTables { get; set; } = null!;



        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var sss = GenerateSeeds.GetOrganizations();
            modelBuilder.Entity<Organization>().HasData(sss);
        }

    }
}
