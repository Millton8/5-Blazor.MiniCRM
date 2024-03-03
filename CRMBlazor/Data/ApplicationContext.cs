using CRMBlazor.Models;
using CRMBlazor.Models.Statistics;
using Microsoft.EntityFrameworkCore;

namespace CRMBlazor.Data
{
    public class ApplicationContext : DbContext
    {
        readonly IConfiguration config;
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<AdvertisingSource> AdvertisingSources { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<AdvertiseStatatistics> AdvStats { get; set; }
        public DbSet<AdvertiseStatatisticsByJobTypes> AdvStatsByJob { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration config)
    : base(options)
        {
            this.config = config;
               // создаем базу данных при первом обращении
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
