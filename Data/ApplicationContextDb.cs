using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace chimneys.Data
{
    public class ApplicationContextDb : DbContext
    {
        public DbSet<Variants> Variants { get; set; }

        public DbSet<Name_variants> Name_var { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=chimneys.db");
        }
    }
}

