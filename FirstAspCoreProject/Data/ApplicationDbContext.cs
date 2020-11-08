using FirstAspCoreProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
namespace FirstAspCoreProject.Data
{
    //to klasa dla naszej bazy danych. Podajemy ją w Startup.cs i rejestrujemy tam w dependency injection container (patrz: Startup.cs)
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> ApplicationType { get; set; }
    }
}
