﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WorkFlow.Entities;
namespace WorkFlow
{
    public class WorkFlowContext : DbContext
    {
        public WorkFlowContext(DbContextOptions<WorkFlowContext> options)
            : base(options)
        { }


        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowActivity> FlowActivities  { get; set; }
        public DbSet<FlowType> FlowTypes { get; set; }
        public DbSet<FlowActivityType> FlowActivityTypes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.EnableAutoHistory(null);
        }
    }
}
