using DavidProject.Data;
using DavidProject.Models;
using DavidProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Repositories
{
	public class itemRepository : IItem
	{
		private readonly DataContext context;
		public itemRepository(DataContext context)
		{
			this.context = context;
		}
		public void Create(item item)
		{
			context.Set<item>().Add(item);
			context.SaveChanges();
		}

		public void Delete(int id)
		{
			var entity = context.Set<item>().Find(id);
			context.Items.Remove(entity);
			context.SaveChanges();

		}

		public item GetItem(int? id)
		{
			var entity = context.Items.FirstOrDefault(x => x.Id == id);
			return entity;
		}

		public IEnumerable<item> GetItems()
		{
			return context.Items.ToList();
		}
	}
}
