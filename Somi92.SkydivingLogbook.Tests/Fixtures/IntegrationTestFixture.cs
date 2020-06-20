using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Somi92.SkydivingLogbook.Core.Data;
using Somi92.SkydivingLogbook.Web;

namespace Somi92.SkydivingLogbook.Tests.IntegrationTests.Fixtures
{
    public class IntegrationTestFixture : WebApplicationFactory<Startup>, IDisposable
    {
        private readonly Checkpoint _checkpoint;
        protected IConfiguration Configuration { get; }
        public readonly HttpClient Client;
        // public SkydivingLogbookContext Context;

        public IntegrationTestFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            Client = CreateClient();

            _checkpoint = new Checkpoint
            {
                DbAdapter = DbAdapter.Postgres
            };
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remove the app's db context registration.
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<SkydivingLogbookContext>));

                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                // Register db context for test database..
                services.AddDbContext<SkydivingLogbookContext>(opt =>
                    opt.UseNpgsql(Configuration.GetConnectionString("SkydivingLogbookTest")));

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database.
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<SkydivingLogbookContext>();
                    // var logger = scopedServices
                    //     .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.Migrate();

                    // TODO: Setup database seeding and logging in tests
                    db.Users.Add(new Core.Domain.User { Id = 1, Name = "John Doe", Email = "john.doe@email.com" });
                    db.SaveChanges();

                    // try
                    // {
                    //     // Seed the database with test data.
                    //     Utilities.InitializeDbForTests(db);
                    // }
                    // catch (Exception ex)
                    // {
                    //     logger.LogError(ex, "An error occurred seeding the " +
                    //         "database with test messages. Error: {Message}", ex.Message);
                    // }
                }
            });
        }

        public async new void Dispose()
        {
            base.Dispose();

            using (var conn = new NpgsqlConnection(Configuration.GetConnectionString("DefaultAdmin")))
            using (var cmd = new NpgsqlCommand("DROP DATABASE \"SkydivingLogbook_Test\";", conn))
            {
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
