using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDevextremeAspCoreProject.Models
{
    public class ScheduleTime
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("TimeTable")]
        [Required]
        public Guid IdTimeTable { get; set; }

        public TimeTable? TimeTable { get; set; }

        [ForeignKey("Client")]
        public Guid? IdClient { get; set; }

        [Required]
        [ForeignKey("Service")]
        public Guid IdService { get; set; }

        public Service? Service { get; set; }

        public Client? Client { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
