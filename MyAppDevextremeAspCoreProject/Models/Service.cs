using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Service
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        public double Price { get; set; }

        [Required]
        public TimeSpan TimeWork { get; set; }

        public List<EmployeeService> EmployeeServices { get; set; } = new();
    }
}
