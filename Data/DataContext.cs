﻿using DavidProject.Models;
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
	}
}
