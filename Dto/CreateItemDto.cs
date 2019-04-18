using DavidProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Dto
{
	public class UserDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime DueDate { get; set; }
		public bool isCompleted { get; set; } = false;
		public Priority Priority { get; set; }
	}
}
