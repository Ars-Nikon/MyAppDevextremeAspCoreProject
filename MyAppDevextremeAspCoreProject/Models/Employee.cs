using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Patronymic { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(11)]
        [RegularExpression(@"79\d{9}", ErrorMessage = "Формат должен быть 79*********")]
        public string Phone { get; set; } = null!;

        [Required]
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public List<Guid> GuidFilials { get; set; } = new();

        public List<EmployeeFilial> EmployeeFilials { get; set; } = new();
        public List<EmployeeService> EmployeeServices { get; set; } = new();
    }
}
