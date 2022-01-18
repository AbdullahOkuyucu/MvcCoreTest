using Microsoft.EntityFrameworkCore;
using MvcCoreTest.Entiti;
using MvcCoreTest.Models;

namespace MvcCoreTest.Context
{
    public class ArabaContext : DbContext
    {
        public DbSet<Araba> Arabalar { get; set; }
        public DbSet<Model> Modellers { get; set; }
        public DbSet<Uretici> Ureticiler{ get; set; } 
        public DbSet<Aciklama> Aciklamalar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"server=.\sqlexpress;database=Cars;trusted_connection=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<MvcCoreTest.Models.ArabaModel> ArabaModel { get; set; }
    }
}
