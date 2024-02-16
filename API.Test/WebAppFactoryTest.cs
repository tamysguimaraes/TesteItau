using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Products.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace API.Test
{
    public class WebAppFactoryTest<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var existContext = services.SingleOrDefault(d => d.ServiceType == typeof(APIContext));
                    if (existContext != null)
                        services.Remove(existContext);

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                    services.AddDbContext<APIContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDBTest");
                        options.UseInternalServiceProvider(provider);
                    });

                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();
                    var scopedService = scope.ServiceProvider;

                    var database = scopedService.GetRequiredService<APIContext>();
                    database.Database.EnsureDeleted();
                });

        }
    }
}
