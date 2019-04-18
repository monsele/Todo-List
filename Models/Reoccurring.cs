using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Models
{
	public class Reoccurring
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}
