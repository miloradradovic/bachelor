using System;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer
{
    public class PostgreSqlContext : DbContext
    {
        
        public DbSet<User> Users { get; set; } 
        public DbSet<AdditionalJobAdInfo> AdditionalJobAdInfos { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<HandyMan> HandyMen { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Trade> Trades { get; set; }

        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)  
        {  
        }
        
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);
            //builder.Entity<Person>().ToTable("People");
            //builder.Entity<User>().ToTable("Users");
        }  
  
        public override int SaveChanges()  
        {  
            ChangeTracker.DetectChanges();  
            return base.SaveChanges();  
        }  
    }
}