using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [Required]
        public int INN { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        [MaxLength(250)]
        public string FullNameOwner { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Address { get; set; } = null!;
    }
}
