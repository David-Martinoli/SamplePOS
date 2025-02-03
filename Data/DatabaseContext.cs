using Microsoft.EntityFrameworkCore;
using SamplePOS_ServerSide.Models;

namespace SamplePOS_ServerSide.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}