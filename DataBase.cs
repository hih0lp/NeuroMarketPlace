using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProjectC_NeuroWeb.Modules.ProductModule.Core;
using PetProjectC_NeuroWeb.Modules.UserModule.Core;

namespace PetProjectC_NeuroWeb
{
    public class DataBase : DbContext
    {
        public DataBase()
        {
            Database.EnsureCreated();               
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost; user=root; password=aternosru123; database=database;", 
                ServerVersion.AutoDetect("server=localhost; user=root; password=aternosru123; database=database;")); //input path
        }
    }
}
