using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class EmployeeService
    {
        [Required]
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [Required]
        [ForeignKey("Service")]
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
