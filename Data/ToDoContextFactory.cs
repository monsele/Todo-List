using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Data
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<HangFireContext>
    {
        public ToDoContextFactory()
        {
        }

        public HangFireContext CreateDbContext(string[] args)
        {
            var conn = "Server=DESKTOP-92LM3ID\\SQLEXPRESS;Database=HangFireDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            var builder = new DbContextOptionsBuilder<HangFireContext>();
            builder.UseSqlServer(conn);
            return new HangFireContext(builder.Options);
        }


    }
}
