using Hanselman.Services;
using Microsoft.Extensions.DependencyInjection;
using Shiny;
using Shiny.Jobs;

namespace Hanselman
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var job = new JobInfo
            {
                Identifier = nameof(BackgroundRefreshJob),
                Type = typeof(BackgroundRefreshJob),
                BatteryNotLow = true,
                DeviceCharging = true,
                RequiredInternetAccess = InternetAccess.Unmetered,
                Repeat = true
            };

            services.RegisterJob(job);
        }
    }
}
