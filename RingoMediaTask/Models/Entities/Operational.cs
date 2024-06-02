using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RingoMediaTask.Models.Entities
{
    public class Operational
    {
        [Key]
        public int IdOperational { get; set; }
        public required string Name { get; set; }
        public required byte[] Logo { get; set; }
        // Foreign key
        [ForeignKey("Management")]
        public int ManagementId { get; set; }
        // Navigation property
        public virtual Management Management { get; set; }
        // Foreign key
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        // Navigation property
        public virtual Department Department { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
