using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PentonixAssesment.Models
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("pentonix") { }

        
        public DbSet<Registration> Registration { get; set; }
       
        
        
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<Registration>().ToTable("Registration");
           

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }

        
    }
}