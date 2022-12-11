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
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<TimeTable> TimeTables { get; set; } = null!;
        public DbSet<EmployeeService> EmployeeServices { get; set; } = null!;




        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
                   Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeService>().HasKey(sc => new { sc.EmployeeId, sc.ServiceId });
            modelBuilder.Entity<EmployeeFilial>().HasKey(sc => new { sc.EmployeeId, sc.FilialId });

            modelBuilder.Entity<Organization>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Filial>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Employee>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Service>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Client>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<TimeTable>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ScheduleTime>()
           .Property(b => b.CreatedDate)
           .HasDefaultValueSql("getdate()");

            modelBuilder.GenerateData();
        }

    }
}
