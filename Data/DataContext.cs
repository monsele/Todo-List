using DavidProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Data
{
	public class DataContext: IdentityDbContext
	{
		public DataContext(DbContextOptions<DataContext>options):base(options)
		{
		}
		public DbSet<item> Items { get; set; }
        public DbSet<MonthlyTask> Monthlies { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}

    public class HangContext : IDesignTimeDbContextFactory<DataContext>
    {
        DataContext IDesignTimeDbContextFactory<DataContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer("Server=DESKTOP-92LM3ID\\SQLEXPRESS;Database=David_DB;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new DataContext(builder.Options);
        }
    }
}
