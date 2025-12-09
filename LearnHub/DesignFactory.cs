using LearnHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LearnHub
{
    
    public class DesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite(connectionString);

           
            return new ApplicationDbContext(builder.Options);
        }
    }
}