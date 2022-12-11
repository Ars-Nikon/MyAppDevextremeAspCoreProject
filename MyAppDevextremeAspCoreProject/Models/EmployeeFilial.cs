using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class EmployeeFilial
    {
        [Required]
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        [ForeignKey("Filial")]
        public Guid FilialId { get; set; }
        public Filial? Filial { get; set; }
    }
}
