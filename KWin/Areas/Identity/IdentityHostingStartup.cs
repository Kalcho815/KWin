using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(KWin.Areas.Identity.IdentityHostingStartup))]
namespace KWin.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}