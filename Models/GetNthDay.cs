using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Models
{
    public class GetNthDay
    {
        
        MonthlyTask task = new MonthlyTask();
        
        public int GetNthDayOfMonth(int nth, DayOfWeek dayOfWeek)
        {
            int day = (int)dayOfWeek;
            DateTime now = DateTime.Now;                                            //get now, in case we span days            
            DateTime firstDayOfMonthDate = new DateTime(now.Year, now.Month, 1);    //now get the first day of month
            DayOfWeek firstDayOfWeek = firstDayOfMonthDate.DayOfWeek;               //now get the day of week
            int firstOfMonthDay = (int)firstDayOfWeek;
            int returnDay = 0;
            int x = (int)System.DayOfWeek.Sunday;
            //now compare day to curr day (sunday==0 fyi)
            if (firstOfMonthDay == day)
            {
                //the first day of the month is what we need, so do the simple math
                returnDay = 1 + (7 * (nth - 1));
            }
            else if (firstOfMonthDay < day) //( ex: wed < thurs )
            {
                //ex.  3rd thurs of Feb 2012 - 1 + (7 *(3-1)) + (4-3) = 16 (16th day of month)
                returnDay = 1 + (7 * (nth - 1)) + (day - firstOfMonthDay);
            }
            else //(firstday > day)  ex: wed > mon )
            {
                //ex.  3rd mon of Feb 2012 - 1 + (7 *(3-1)) + (3+1) = 20 (20th day of month)
                returnDay = 1 + (7 * (nth - 1)) + (firstOfMonthDay + day) + 1; //add one at end to fix 0 offset of DayOfWeek
            }

            //in this case we need to make sure we are not in an invalid date scenario, if so back it out by 7 days
            if (nth == 5)
            {
                DateTime outDate;
                DateTime.TryParse(String.Format("{0}-{1}-{2}", now.Year, now.Month, returnDay), out outDate);

                //if it was bad, minus 7 and go
                if (outDate == DateTime.MinValue || outDate == null)
                    returnDay -= 7;
            }

            return returnDay;
        }
 
    }
}
