using DavidProject.Models;
using System.Collections.Generic;

namespace DavidProject.Services
{
	public interface IItem
	{
		IEnumerable<item> GetItems();
		item GetItem(int? id);
		void Create(item item);
		void Delete(int id);
        void Update(int? id, item item);
		IEnumerable<item> GetItemByUser(ApplicationUser user,string id);
	}
}
