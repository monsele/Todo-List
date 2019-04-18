using DavidProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
		public DbSet<Reoccurring>things{ get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		//protected override void OnModelCreating(ModelBuilder builder)
		//{
		//	// add your own confguration here
		//	base.OnModelCreating(builder);
		//	builder.Entity<ApplicationUser>().HasMany(x => x.items).WithOne(x => x.ApplicationUser).IsRequired();
		//	builder.Entity<ApplicationUser>().HasMany(x => x.reoccurring).WithOne(x => x.ApplicationUser).IsRequired(false);

		//}

		
	}
	
}
