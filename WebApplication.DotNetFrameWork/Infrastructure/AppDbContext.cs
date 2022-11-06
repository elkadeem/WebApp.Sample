using System.Data.Entity;
using WebApplication.DotNetFrameWork.Models;

namespace WebApplication.DotNetFrameWork.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}