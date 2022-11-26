using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(10)]
        [RegularExpression(@"\d{10}", ErrorMessage = "Формат должен быть из 13 символов")]
        public string INN { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [RegularExpression(@"79\d{9}", ErrorMessage = "Формат должен быть 79*********")]
        [MaxLength(11)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250)]
        public string FullNameOwner { get; set; } = null!;

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(250)]
        public string Address { get; set; } = null!;
    }
}
