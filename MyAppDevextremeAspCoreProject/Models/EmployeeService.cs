using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class EmployeeService
    {
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [ForeignKey("Service")]
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
