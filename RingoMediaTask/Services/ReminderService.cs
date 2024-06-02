
using Humanizer;
using RingoMediaTask.Data;
using RingoMediaTask.Models.Entities;
using RingoMediaTask.Services.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RingoMediaTask.Services
{
    public class ReminderService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ReminderService> _logger;
        private readonly IConfiguration _configuration;

        public ReminderService(
            ILogger<ReminderService> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"ReminderService is starting.");
            stoppingToken.Register(() =>
                _logger.LogInformation($" ReminderService background task is stopping."));
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AdminDbContext>();
                        var emailNotificationService = scope.ServiceProvider.GetRequiredService<IEmailNotification>();
                        //var reminders = dbContext.Reminders.Where(x => x.IsProcessing == 1).ToList();
                        var now = DateTime.Now;
                        var reminderLesssTime = dbContext.Reminders.Where(x => x.IsProcessing == 1 &&(x.ReminderDateTime <= now.AddMinutes(5))).ToList();
                        foreach (var email in reminderLesssTime)
                        {
                            var response = emailNotificationService.SendEmailAsync(email).Result;
                            _logger.LogInformation($"Reminder status:{response.StatusCode}");
                            _logger.LogInformation($"Reminder response:{response.Body}");
                            if (response.IsSuccessStatusCode)
                            {
                                email.IsProcessing = 2;
                                var updateResult = dbContext.Reminders.Update(email);
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ReminderService task failed with exception : {ex.Message}");
                }


                _logger.LogInformation($"ReminderService task doing background work.");

                await Task.Delay(2000, stoppingToken);
            }

            _logger.LogInformation($"ReminderService background task is stopping.");
        }
    }
}
