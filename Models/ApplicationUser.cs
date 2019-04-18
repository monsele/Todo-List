using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DavidProject.Models
{
	public class ApplicationUser:IdentityUser
	{
		//public int itemId { get; set; }
		public IEnumerable<item> items { get; set; }
		//public int ReoccurringId { get; set; }
		public IEnumerable<Reoccurring> reoccurring { get; set; }
	}
}
