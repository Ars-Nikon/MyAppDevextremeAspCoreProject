using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string INN { get; set; } = null!;

        [Required]
        [RegularExpression(@"79\d{9}", ErrorMessage = "Формат должен быть 79*********")]
        public string Phone { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string FullNameOwner { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Address { get; set; } = null!;
    }
}
