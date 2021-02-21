using ChessMeters.Core.Database;
using ChessMeters.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessMeters.Core.Configuration
{
    public class DatabaseConfigurationProvider : ConfigurationProvider
    {
        private readonly Dictionary<string, string> configValues;

        public DatabaseConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
            configValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "EmailConfiguration:Username", null },
                { "EmailConfiguration:Password", null },
                { "EmailConfiguration:SmtpServer", "smtpout.europe.secureserver.net" },
                { "EmailConfiguration:Port", "465" },
                { "Authentication:Google:ClientId", null },
                { "Authentication:Google:ClientSecret", null },
                { "Authentication:Facebook:AppId", null },
                { "Authentication:Facebook:AppSecret", null }
            };
        }

        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ChessMetersContext>();
            OptionsAction(builder);

            using var dbContext = new ChessMetersContext(builder.Options, new OperationalStoreOptionsMigrations());
            dbContext.Database.EnsureCreated();

            Data = dbContext.Settings.Count() != configValues.Count ? CreateAndSaveDefaultValues(dbContext) :
                dbContext.Settings.ToDictionary(c => c.Id, c => c.Value);
        }

        private IDictionary<string, string> CreateAndSaveDefaultValues(ChessMetersContext dbContext)
        {
            var settingsToCreate = configValues.Where(x => !dbContext.Settings.Any(s => s.Id == x.Key));
            dbContext.Settings.AddRange(settingsToCreate.Select(kvp => new Setting
            {
                Id = kvp.Key,
                Value = kvp.Value
            }).ToArray());

            dbContext.SaveChanges();
            return dbContext.Settings.ToDictionary(x => x.Id, x => x.Value);
        }
    }
}
