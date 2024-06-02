using System.ComponentModel.DataAnnotations;

namespace RingoMediaTask.Models.Entities
{
    public class Department
    {
        [Key]
        public int IdDepartment { get; set; }
        public required string Name { get; set; }
        public required byte[] Logo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
