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

        public DbSet<CategoryModel> CategoryModels { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost; user=root; password=aternosru123; database=DBTEST;", 
                ServerVersion.AutoDetect("server=localhost; user=root; password=aternosru123; database=DBTEST;"));
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DataBaseProductModel>()
        //        .HasOne(p => p.Category)
        //        .WithMany(s => s.Products)
        //        .HasForeignKey(e => e.CategoryName)
        //        .HasPrincipalKey(r => r.Name); 
        //}
    }
}
