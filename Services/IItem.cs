using DavidProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Services
{
	public interface IItem
	{
		IEnumerable<item> GetItems();
		item GetItem(int? id);
		void Create(item item);
		void Delete(int id);
	}
}
