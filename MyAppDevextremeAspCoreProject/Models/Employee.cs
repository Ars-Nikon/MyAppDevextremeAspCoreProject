using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Employee
    {
        [Key]
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
        public int Phone { get; set; }

        [Required]
        public Status Status { get; set; }

        public List<Filial> Filials { get; set; } = new();
        public List<Speciality> Specialities { get; set; } = new();
    }

    public enum Status
    {
        Hidden = 0,
        Works = 1,
        Fired = 2,
    }

}
