using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PublisherData;
using System.Linq;

namespace Tests

//this class is based on Microsoft docs sample from:
//https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0#customize-webapplicationfactory
{
    public class CustomWebApplicationFactory<TStartup>
      : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var pubContextdescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PubContext>));
                services.Remove(pubContextdescriptor);
                services.AddDbContext<PubContext>(options =>
                {
                    options.UseInMemoryDatabase("TheInteractionTest");
                });
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<PubContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    db.Database.EnsureCreated(); //remember, EnsureCreated runs HasData methods!
                }
            });
        }
    }
}
