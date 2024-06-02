using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Models;
using RingoMediaTask.Models.Entities;

namespace RingoMediaTask.Controllers
{
    public class OperationalsController : Controller
    {
        private readonly AdminDbContext _context;

        public OperationalsController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: Operationals
        public async Task<IActionResult> Index()
        {
            var adminDbContext = _context.Operationals.Include(o => o.Department).Include(o => o.Management);
            return View(await adminDbContext.ToListAsync());
        }

        // GET: Operationals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operational = await _context.Operationals
                .Include(o => o.Department)
                .Include(o => o.Management)
                .FirstOrDefaultAsync(m => m.IdOperational == id);
            if (operational == null)
            {
                return NotFound();
            }

            return View(operational);
        }

        // GET: Operationals/Create
        public IActionResult Create()
        {
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment");
            //ViewData["ManagementId"] = new SelectList(_context.Managements, "IdManagement", "IdManagement");
            List<Department> departments = _context.Departments
                              .Where(x => x.IsActive).ToList();
            List<Management> managements = _context.Managements
                              .Where(x => x.IsActive).ToList();
            return View(new OperationalDTO() { Departments = departments, Managements = managements  });
        }

        // POST: Operationals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( OperationalDTO model,IFormFile Logo)
        {
            ModelState.Remove("operational.Logo");
            ModelState.Remove("operational.Department");
            ModelState.Remove("operational.Management");
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Logo.CopyToAsync(memoryStream);
                    model.operational.Logo = memoryStream.ToArray();
                }
                _context.Add(model.operational);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment", model.operational.DepartmentId);
            ViewData["ManagementId"] = new SelectList(_context.Managements, "IdManagement", "IdManagement", model.operational.ManagementId);
            return View(model);
        }

        // GET: Operationals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operational = await _context.Operationals.FindAsync(id);
            if (operational == null)
            {
                return NotFound();
            }
            List<Department> departments = _context.Departments
                              .Where(x => x.IsActive).ToList();
            List<Management> managements = _context.Managements
                              .Where(x => x.IsActive).ToList();
            return View(new OperationalDTO() { Departments = departments, Managements = managements,operational = operational });
        }

        // POST: Operationals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OperationalDTO model,IFormFile Logo)
        {
            if (id != model.operational.IdOperational)
            {
                return NotFound();
            }
            ModelState.Remove("operational.Logo");
            ModelState.Remove("operational.Department");
            ModelState.Remove("operational.Management");
            if (ModelState.IsValid)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Logo.CopyToAsync(memoryStream);
                        model.operational.Logo = memoryStream.ToArray();
                    }
                    _context.Update(model.operational);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationalExists(model.operational.IdOperational))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment", model.operational.DepartmentId);
            ViewData["ManagementId"] = new SelectList(_context.Managements, "IdManagement", "IdManagement", model.operational.ManagementId);
            return View(model);
        }

        // GET: Operationals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operational = await _context.Operationals
                .Include(o => o.Department)
                .Include(o => o.Management)
                .FirstOrDefaultAsync(m => m.IdOperational == id);
            if (operational == null)
            {
                return NotFound();
            }

            return View(operational);
        }

        // POST: Operationals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operational = await _context.Operationals.FindAsync(id);
            if (operational != null)
            {
                _context.Operationals.Remove(operational);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationalExists(int id)
        {
            return _context.Operationals.Any(e => e.IdOperational == id);
        }

        [HttpGet]
        public JsonResult GetManagementsByDepartment(int departmentId)
        {
            var managements = _context.Managements
                                        .Where(m => m.DepartmentId == departmentId)
                                        .Select(m => new { m.IdManagement, m.Name })
                                        .ToList();
            return Json(managements);
        }
    }
}
