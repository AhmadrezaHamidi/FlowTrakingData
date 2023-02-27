using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TrackingDataApi;

namespace WorkFlow
{
    public class WorkFlowDbContext : DbContext
    {
        public DbSet<MyIdentity> MyIdentities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(typeof(BaseEntityTypeConfiguration()));
            modelBuilder.ApplyConfiguration(new MyIdentityConfiguration());
        }
    }
}
