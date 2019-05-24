using DavidProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Services
{
    public class RemindMe
    {
        private readonly IMonthlies task;
        public RemindMe(IMonthlies task)
        {
            this.task = task;
        }
        public async Task<JsonResult> Reminder()
        {
            var bg = new GetNthDay();
            var results =  task.GetItems();
            foreach (var item in results)
            {
                 bg.GetNthDayOfMonth(item.nth, item.Day);
                var ans =  task.GetItems().Where(x => x.Day == item.Day && x.nth == item.nth).ToList();
                return new JsonResult(ans);
            }
            return new JsonResult(results);
        }
    }
}
