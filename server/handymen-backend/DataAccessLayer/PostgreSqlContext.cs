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
        public DbSet<HandyMan> HandyMen { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<JobAd> JobAd { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)  
        {  
        }
        
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);
            builder.HasSequence<int>("person_seq")
                .StartsAt(1).IncrementsBy(1);
            builder.Entity<Administrator>()
                .Property(x => x.Id)
                .UseHiLo("person_seq");
            builder.Entity<User>()
                .Property(x => x.Id)
                .UseHiLo("person_seq");
            builder.Entity<HandyMan>()
                .Property(x => x.Id)
                .UseHiLo("person_seq");
        }  
  
        public override int SaveChanges()  
        {  
            ChangeTracker.DetectChanges();  
            return base.SaveChanges();  
        }  
    }
}