using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Filial
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [Required]
        public int PhoneAdmin { get; set; }

        [Required]
        public TimeSpan BeginningOfWork { get; set; }

        [Required]
        public TimeSpan EndOfWork { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; } = null!;

        [Required]
        [ForeignKey("Organization")]
        public Guid IdOrganization { get; set; }

        public Organization? Organization { get; set; }
        public List<Employee> Employees { get; set; } = new();

    }
}
