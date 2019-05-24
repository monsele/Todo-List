using DavidProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Services
{
    public interface IMonthlies
    {
        IEnumerable<MonthlyTask> GetItems();
        MonthlyTask GetItem(int? id);
        void Create(MonthlyTask item);
        void Delete(int? id);
        void Update(int? id, MonthlyTask item);
        IEnumerable<MonthlyTask> GetItemByUser(ApplicationUser user, string id);
        IEnumerable<MonthlyTask> Job();
    }
}

