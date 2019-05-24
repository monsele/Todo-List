using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Models
{
	public enum Priority
	{
		High, Medium, Low
	}
	public class item
	{
		public int Id { get; set; }
        [Required]
		public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Date)]
		public DateTime DueDate { get; set; }
		public bool isCompleted { get; set; } = false;
		public Priority Priority { get; set; }  
		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

        
    }
}
