using DogsHouse.Database.Model;
using DogsHouse.Database.Utility;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Database
{
    public class DogsHouseContext : DbContext
    {
        public virtual DbSet<Dog> Dogs { get; set; }
        public DogsHouseContext() { }
        public DogsHouseContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStrings.DefaultConnection);
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dog>()
                        .HasKey(x => x.Id);

            modelBuilder.Entity<Dog>()
                        .HasIndex(x => x.Name)
                        .IsUnique();
        }
    }
}
