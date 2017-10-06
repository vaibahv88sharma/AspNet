using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SamuraiAppCore.Domain;

namespace SamuraiAppCore.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext(DbContextOptions<SamuraiContext> options)
          : base(options)
        {

        }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //    "Server = SPAPP05P-BRO; Database = SamuraiDataCore; Trusted_Connection = True; "
            //);
            //  "Server = (localdb)\\mssqllocaldb; Database = SamuraiData; Trusted_Connection = True; ");
        }
    }

    // required when local database deleted
    public class ToDoContextFactory : IDesignTimeDbContextFactory<SamuraiContext>
    {
        public SamuraiContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SamuraiContext>();
            builder.UseSqlServer("Server = SPAPP05P-BRO; Database = SamuraiDataCoreWeb; Trusted_Connection = True;");
            return new SamuraiContext(builder.Options);
        }
    }

}
