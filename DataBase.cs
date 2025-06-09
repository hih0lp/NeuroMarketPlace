using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using PetProjectC_NeuroWeb.Models.CategoryModels;
using PetProjectC_NeuroWeb.Models.Products;

namespace NeuroWeb
{
    public class DataBase : DbContext
    {
        public DataBase()
        {
            Database.EnsureCreated();
        }

        public DbSet<WildberriesCategoryModel> CategoryModels { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost; user=root; password=aternosru123; database=DBTEST;", 
                ServerVersion.AutoDetect("server=localhost; user=root; password=aternosru123; database=DBTEST;"));
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProductModel>()
                .HasOne(p => p.Category)
                .WithMany(s => s.Products)
                .HasForeignKey(e => e.CategoryName);
                //.HasPrincipalKey(r => r.Name);
        }
    }
}
