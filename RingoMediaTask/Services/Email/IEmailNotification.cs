using RingoMediaTask.Models.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RingoMediaTask.Services.Email
{
    public interface IEmailNotification
    {
        public Task<Response> SendEmailAsync(Reminder reminder);
        public string GenerateMeetingReminderEmail(string receiverName, DateTime meetingStartTime);
    }
}
