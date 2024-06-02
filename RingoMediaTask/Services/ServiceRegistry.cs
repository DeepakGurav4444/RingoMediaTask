using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Services.Email;

namespace RingoMediaTask.Services
{
    public class ServiceRegistry
    {
        public void ConfigureDependencies(IServiceCollection services)
        {
            #region Services
            services.AddScoped<IEmailNotification,EmailNotification>();
            #endregion

            #region Hosted Service
            services.AddHostedService<ReminderService>();
            #endregion
        }
    }
}
