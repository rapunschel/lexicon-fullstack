using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;
using static System.Net.WebRequestMethods;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring =
              "AccountEndpoint = https://somerespirce.documents.azure.com:443;AccountKey=yoursecretkey";
            connectionstring=  "AccountEndpoint=https://pluralsightdemos.documents.azure.com:443;AccountKey=jQx9PplqrStEB6jUkEAq9cS9Q3ggWbKQsqq3f9PaZ9To3O74fJYMhZFgQici65iaUdd9dIX0xiKazjo3aJ8W8A==;";
            optionsBuilder.UseCosmos(connectionstring, "EFCore9")
            .LogTo(Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information)
           .EnableSensitiveDataLogging();
        }
    }
}
