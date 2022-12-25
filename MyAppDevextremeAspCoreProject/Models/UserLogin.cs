using System.ComponentModel.DataAnnotations;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class UserLogin
    {
        [Required]
        [MinLength(4)]
        [MaxLength(36)]
        public string Login { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(36)]
        public string Password { get; set; } = string.Empty;
    }
}
