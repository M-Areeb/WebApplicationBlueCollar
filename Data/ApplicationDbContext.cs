using Microsoft.EntityFrameworkCore;
using WebApplicationBlueCollar.Models;

namespace WebApplicationBlueCollar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Job> Jobs { get; set; }
    }
}
