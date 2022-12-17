using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class FullScheduleView
    {
        [Required]
        public Guid? IdTimeTable { get; set; }

        [Required]
        public DateTime DateVisit { get; set; }

        [Required]
        public Guid IdEmployee { get; set; }

        [Required]
        public Guid IdFilial { get; set; }

        public Guid? IdClient { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
