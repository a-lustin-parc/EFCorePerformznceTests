using System;
using Microsoft.EntityFrameworkCore;
using Model;

namespace EFCoreContext
{
    public class CoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EFCoreTest;Persist Security Info=True;Integrated Security=SSPI");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Film> Films { get; set; }

        public DbSet<Actor> Actors { get; set; }
    }
}
