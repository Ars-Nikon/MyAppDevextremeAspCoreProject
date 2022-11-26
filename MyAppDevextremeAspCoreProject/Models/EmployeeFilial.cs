using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class EmployeeFilial
    {
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [ForeignKey("Filial")]
        public Guid FilialId { get; set; }
        public Filial Filial { get; set; } = null!;
    }
}
