using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
