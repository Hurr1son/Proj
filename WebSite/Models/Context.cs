using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection") { }

        public DbSet<Car> Cars { get; set; }
        public DbSet <Queue> Queues { get; set; }
        public DbSet <Repair> Repairs { get; set;}

    }
}