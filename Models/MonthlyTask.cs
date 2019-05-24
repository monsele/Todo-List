using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Models
{
    public class MonthlyTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek Day { get; set; }
        public int nth { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
