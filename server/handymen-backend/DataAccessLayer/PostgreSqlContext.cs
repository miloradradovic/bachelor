using System;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer
{
    public class PostgreSqlContext : DbContext
    {
        
        public DbSet<User> Users { get; set; } 
        
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