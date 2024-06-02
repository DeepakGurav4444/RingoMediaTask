using RingoMediaTask.Models.Entities;

namespace RingoMediaTask.Models
{
    public class OperationalDTO
    {
        public OperationalDTO()
        {
            Departments = new List<Department>();
            Managements = new List<Management>();
        }
        public List<Department> Departments { get; set; }
        public List<Management> Managements { get; set; }
        public Operational operational { get; set; }
    }
}
