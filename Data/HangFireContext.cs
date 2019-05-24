using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Data
{
    public class HangFireContext : DbContext
    {
        public HangFireContext(DbContextOptions<HangFireContext> options) : base(options)
        {

        }
    }
}