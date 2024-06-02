using RingoMediaTask.Models.Entities;

namespace RingoMediaTask.Models
{
    public class ManagementDTO
    {
        public ManagementDTO() 
        {
            Departments = new List<Department>();
        }
        public List<Department> Departments  { get; set; }
        public Management management { get; set; }
        
    }
}
