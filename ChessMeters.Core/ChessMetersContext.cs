using Microsoft.EntityFrameworkCore;

namespace ChessMeters.Core
{
    public class ChessMetersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ChessMetersContext(DbContextOptions<ChessMetersContext> options) : base(options)
        {
        }
    }
}
