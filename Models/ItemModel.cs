using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool isCompleted { get; set; } = false;
       public Priority Priority { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
