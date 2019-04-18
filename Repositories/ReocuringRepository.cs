using DavidProject.Data;
using DavidProject.Models;
using DavidProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Repositories
{
	public class ReocuringRepository : IReocurring
	{
		private readonly DataContext context;
		public ReocuringRepository(DataContext context)
		{
			this.context = context;
		}

		public void Create(Reoccurring task)
		{
			context.Set<Reoccurring>().Add(task);
			context.SaveChanges();
		}

		public void Delete(int? id)
		{
			var entity = context.Set<Reoccurring>().Find(id);
			context.Set<Reoccurring>().Remove(entity);
			context.SaveChanges();
		}

		public Reoccurring GetReoccurring(int? id)
		{
			return context.Set<Reoccurring>().FirstOrDefault(x => x.Id == id);		}

		public IEnumerable<Reoccurring> GetReoccurrings()
		{
			return context.Set<Reoccurring>().ToList();
		}

        public void Update(int? id, Reoccurring obj)
        {
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges(); 
        }
    }
}
