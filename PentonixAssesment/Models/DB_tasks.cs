using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PentonixAssesment.Models
{
    public class DB_tasks :DbContext
    {
        public DB_tasks() : base("pentonix") { }

    public DbSet<AssignedTask> AssignedTask { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
       Database.SetInitializer<DB_tasks>(null);
        
        modelBuilder.Entity<AssignedTask>().ToTable("AssignedTask");

        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        base.OnModelCreating(modelBuilder);


    }
   
    }
}