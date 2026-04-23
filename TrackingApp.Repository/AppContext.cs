using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrackingApp.Core.Entites;

namespace TrackingApp.Repository
{
    public class AppContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext>options):base(options)
        {
                
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);  
         }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
        
    }
}
