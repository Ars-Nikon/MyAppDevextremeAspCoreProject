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
        public DbSet<EmployeeFilial> EmployeeFilials { get; set; } = null!;
        public DbSet<FullScheduleView> FullScheduleViews { get; set; } = null!;
        public DbSet<EmployeeFioView> EmployeeFioViews { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;



        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
           // Database.EnsureDeleted();
            if (Database.EnsureCreated())
            {
                Database.ExecuteSqlRaw("Create view EmployeeFioView as SELECT Id, (Surname+' '+Name+' '+Patronymic) as FIO from Employees");
                Database.ExecuteSqlRaw("create view FullScheduleView as SELECT tt.Id as IdTimeTable, tt.Date as DateVisit, tt.IdEmployee as IdEmployee, \r\n tt.IdFilial as IdFilial, sch.Id as IdSchedule, sch.IdClient as IdClient, sch.StartTime as StartTime, sch.EndTime as EndTime, \r\n CASE when (sch.IdClient is not null) then (c.Surname + ' ' + c.Name + ' ' + c.Patronymic + ' ' + ser.Name) ELSE  N'Свободно' end as Text,\r\n CASE when (sch.IdClient is not null) then 1 ELSE 2 end as Color\r\n FROM TimeTables tt\r\n LEFT JOIN ScheduleTimes sch on tt.Id = sch.IdTimeTable\r\n LEFT JOIN Clients c  on c.Id = sch.IdClient\r\n LEFT JOIN Services ser on ser.Id = sch.IdClient");
            }
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

            modelBuilder.Entity<FullScheduleView>()
                .ToView("FullScheduleView")
                .HasNoKey();

            modelBuilder.Entity<EmployeeFioView>()
                 .ToView("EmployeeFioView")
                 .HasNoKey();

            modelBuilder.GenerateData();
        }

    }
}
