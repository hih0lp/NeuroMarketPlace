using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using PetProjectC_NeuroWeb.Models.CategoryModels;
using PetProjectC_NeuroWeb.Models.Products;
using System.Data.Common;
using System.Reflection.Emit;

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
            optionsBuilder.UseMySql("server=localhost; user=root; password=aternosru123; database=databasenahren", 
                ServerVersion.AutoDetect("server=localhost; user=root; password=aternosru123; database=databasenahren;"));
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<WildberriesCategoryModel>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelbuilder.Entity<ProductModel>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryName)  // Внешний ключ в ProductModel
                .HasPrincipalKey(c => c.Name)       // Главный ключ в CategoryModel
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
