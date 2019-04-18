using DavidProject.Data;
using DavidProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject
{
	public class Reminder
	{
		public DataContext context;
		public void  Occur(Reoccurring obj,string id)
		{
			//var time = obj.ApplicationUser.reoccurring.Select(x=>x.);
			var currentUser = context.ApplicationUsers.Where(x => x.Id == id );
			var occ = context.things.Where(x => x.ApplicationUser == currentUser).Select(x=>x.Date==DateTime.Now).ToList();

			
			foreach (var item in occ)
			{
				string message = "pls you have a task today";
			}
		}
	}
}
