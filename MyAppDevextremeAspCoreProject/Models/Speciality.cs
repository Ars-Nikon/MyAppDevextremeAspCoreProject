using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Speciality
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public decimal Price { get; set; }

        [Required]
        public Gender AcceptsGender { get; set; }

        public List<Employee> Employees { get; set; } = new();
    }
    public enum Gender
    {
        Woman = 0,
        man = 1,
        All = 2
    }
}
