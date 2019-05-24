using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Models
{
    public class MonthlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek Day { get; set; }
        public int nth { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
