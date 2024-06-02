using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RingoMediaTask.Models.Entities
{
    public class Reminder
    {
        [Key]
        public int IdReminder { get; set; }
        public required string ReminderFor { get; set; }
        public required string EmailForReminder { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public byte IsProcessing { get; set; } = 1;
    }
}
