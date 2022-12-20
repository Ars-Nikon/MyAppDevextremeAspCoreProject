using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class FullScheduleView
    {
        [Required]
        public Guid IdTimeTable { get; set; }

        [Required]
        public DateTime DateVisit { get; set; }

        [Required]
        public Guid IdEmployee { get; set; }

        [Required]
        public Guid IdFilial { get; set; }

        public Guid? IdSchedule { get; set; }
        public Guid? IdClient { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? Color { get; set; }

        public string? Text { get; set; } = string.Empty;
    }
}
