using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RingoMediaTask.Models.Entities
{
    public class Management
    {
        [Key]
        public int IdManagement { get; set; }
        public required string Name { get; set; }
        public required byte[] Logo { get; set; }
        // Foreign key
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        // Navigation property
        public virtual Department Department { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
