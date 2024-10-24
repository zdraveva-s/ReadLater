using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ReadLater5.Areas.Identity.IdentityHostingStartup))]
namespace ReadLater5.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}