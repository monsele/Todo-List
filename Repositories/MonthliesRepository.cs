using DavidProject.Data;
using DavidProject.Models;
using DavidProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Repositories
{
    public class MonthliesRepository : IMonthlies
    {
        private readonly DataContext context;
        public MonthliesRepository(DataContext context)
        {
            this.context = context;
        }
        public void Create(MonthlyTask item)
        {
            context.Set<MonthlyTask>().Add(item);
            context.SaveChanges();
        }

        public void Delete(int? id)
        {
            var entity = context.Set<MonthlyTask>().Find(id);
            context.Set<MonthlyTask>().Remove(entity);
            context.SaveChanges();
        }

        public MonthlyTask GetItem(int? id)
        {
            var entity = context.Set<MonthlyTask>().FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public IEnumerable<MonthlyTask> GetItemByUser(ApplicationUser user, string id)
        {
            return context.Set<MonthlyTask>().Where(x => x.ApplicationUser == user && x.UserId == id).ToList();
        }

        public IEnumerable<MonthlyTask> GetItems()
        {
            return  context.Set<MonthlyTask>().ToList();
        }

        public IEnumerable<MonthlyTask> Job()
        {
            throw new NotImplementedException();
        }

        public void Update(int? id, MonthlyTask item)
        {
            var entity = context.Set<MonthlyTask>().Find(id);
            entity.Name = item.Name;
            entity.Day = item.Day;
            entity.nth = item.nth;
            context.SaveChanges();
        }

        //public IEnumerable<MonthlyTask> Job()
        //{
        //    var result = context.Set<MonthlyTask>().ToList();

        //    GetNthDay bg = new GetNthDay();
        //    foreach (var item in result)
        //    {
        //        bg.GetNthDayOfMonth(item.nth, item.Day);
        //        var ans = context.Set<MonthlyTask>().Where(x => x.Day == item.Day && x.nth == item.nth).ToList();
        //        return ans;
        //    }
        //    return 

    }

       
    
}
