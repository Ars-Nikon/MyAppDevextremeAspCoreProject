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
        [MaxLength(11)]
        [RegularExpression(@"79\d{9}", ErrorMessage = "Формат должен быть 79*********")]
        public string PhoneAdmin { get; set; } = null!;

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
        public List<EmployeeFilial> EmployeeFilials { get; set; } = new();

    }
}
