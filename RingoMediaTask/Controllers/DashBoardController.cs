using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Models;
using System.Runtime.InteropServices;

namespace RingoMediaTask.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly AdminDbContext _context;

        public DashBoardController(AdminDbContext context)
        {
            _context = context;
        }
        // GET: DashBoardController
        public async Task<ActionResult> Index()
        {
            var departments = await _context.Departments.Where(x => x.IsActive).ToListAsync();
            var managements = await _context.Managements.Where(x => x.IsActive).ToListAsync();
            var operationals = await _context.Operationals.Where(x => x.IsActive).ToListAsync();

            return View(new DashBoardViewModel() 
            {
                DepartmentCount = departments.Count,
                ManagementCount = managements.Count,
                OperationalCount = operationals.Count
            });
        }

    }
}
