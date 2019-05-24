using DavidProject.Data;
using DavidProject.Models;
using DavidProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Repositories
{
	public class itemRepository : IItem
	{
		private readonly DataContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public itemRepository(DataContext context,UserManager<ApplicationUser>userManager)
		{
            this.userManager = userManager;
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

		public IEnumerable<item> GetItemByUser(ApplicationUser user,string id)
		{
			return context.Items.Where(x => x.ApplicationUser == user&&x.UserId==id).ToList();
		}

		public IEnumerable<item> GetItems()
		{
			return context.Items.ToList();
		}
        public void Update(int? id, item obj)
        {
           var entity=context.Items.Find(id);
            entity.Name = obj.Name;
            entity.Priority = obj.Priority;
            entity.isCompleted = obj.isCompleted;
            entity.Description = obj.Description;
            entity.DueDate = obj.DueDate;
            context.Items.Update(entity);
            context.SaveChanges();
        }
    }
}
