using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RingoMediaTask.Models.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RingoMediaTask.Services.Email
{
    public class EmailNotification : IEmailNotification
    {
        private readonly IConfiguration _configuration;

        public EmailNotification(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task<Response> SendEmailAsync(Reminder reminder)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(_configuration["SendGrid:From"], "RINGOMEDIA"),
                Subject = "Reminder for meeting"
            };
            message.AddContent(MimeType.Html,GenerateMeetingReminderEmail(reminder.ReminderFor, reminder.ReminderDateTime));
            message.AddTo(reminder.EmailForReminder);
            Console.WriteLine($"Sending email with payload: \n{message.Serialize()}");
            var response = await new SendGridClient(_configuration["SendGrid:ApiKey"]).SendEmailAsync(message).ConfigureAwait(false);
            Console.WriteLine($"Response: {response.StatusCode}");
            Console.WriteLine(response.Headers);
            return response;
        }

        public string GenerateMeetingReminderEmail(string receiverName,DateTime meetingStartTime)
        {
            // HTML content for the email body
            string emailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Meeting Reminder</title>
            </head>
            <body>
                <p>Dear {receiverName},</p>
                <p>This is a friendly reminder that you have a meeting scheduled to start in 5 minutes.</p>
                <p>Meeting Details:</p>
                <ul>
                    <li>Meeting Start Time: {meetingStartTime}</li>
                    <li>Meeting Location: Conference Room</li>
                    <li>Agenda: Discuss project progress</li>
                </ul>
                <p>Please be punctual and prepared for the meeting.</p>
                <p>Best regards,<br>Your Organization</p>
            </body>
            </html>";

            return emailBody;
        }
    }
}
