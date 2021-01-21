using ChessMeters.Core.Engines.Enums;
using ChessMeters.Core.Entities;
using ChessMeters.Core.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChessMeters.Core.Database
{
    public class ChessMetersContext : ApiAuthorizationDbContext<User>
    {
        public DbSet<Report> Reports { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<TreeMove> TreeMoves { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<EngineEvaluation> EngineEvaluations { get; set; }

        public ChessMetersContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SeedEnumValues<Engine, EngineEnum>(e => e);

            builder.Entity<TreeMove>().Property(p => p.Color).HasComputedColumnSql("IF(ISNULL(FullPath), 0, MOD(LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', '')), 2) = 0)");
        }
    }
}
