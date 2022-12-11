using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Client
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
        public string? Phone { get; set; }

        [MaxLength(50)]
        [RegularExpression(@"^[\d\w._-]+@[\d\w._-]+\.[\w]+$", ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
