using Abdt.Loyal.UserManager.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("MigrationSettings.json", false, true)
    .AddEnvironmentVariables()
    .Build();

var connectionString = configuration.GetConnectionString("PostgresConnection");

var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
optionsBuilder.UseNpgsql(connectionString);

var context = new UserContext(optionsBuilder.Options);
await context.Database.MigrateAsync();
