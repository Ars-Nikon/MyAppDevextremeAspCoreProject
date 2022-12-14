using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class TimeTable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public Guid IdEmployee { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        [ForeignKey("Filial")]
        public Guid IdFilial { get; set; }
        public Filial? Filial { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

    }
}
