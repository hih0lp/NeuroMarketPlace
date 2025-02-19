using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NeuroWeb.Models;

namespace NeuroWeb
{
    public class DataBase : DbContext
    {
        public DataBase()
        {
            Database.EnsureCreated();
        }

        public DbSet<DataBaseCategoryModel> CategoryModels { get; set; }
        public DbSet<DataBaseProductModel> ProductModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost; user=root; password=aternosru123; database=DBTEST", 
                ServerVersion.AutoDetect("server=localhost; user=root; password=aternosru123; database=DBTEST"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataBaseProductModel>()
                .HasOne(p => p.category)
                .WithMany(s => s.products)
                .HasForeignKey(e => e.categoryName); //добавить связь
        }
    }
}
