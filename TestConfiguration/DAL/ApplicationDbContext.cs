using Microsoft.EntityFrameworkCore;
using TestConfiguration.Models;

namespace TestConfiguration.DAL
{
    public class ApplicationDbContext  :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<TestConfig>  TestConfigs  { get; set; }
        public DbSet<TestConfigDetail> TestConfigDetails  { get; set; }
    }
}
