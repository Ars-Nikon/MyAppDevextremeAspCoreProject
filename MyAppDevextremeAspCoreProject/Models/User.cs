using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(36)]
        public string Login { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordHash { get; set; } = new byte[0];
    }
}
