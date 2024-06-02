using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Models;
using RingoMediaTask.Models.Entities;

namespace RingoMediaTask.Controllers
{
    public class ManagementsController : Controller
    {
        private readonly AdminDbContext _context;

        public ManagementsController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: Managements
        public async Task<IActionResult> Index()
        {
            var adminDbContext = _context.Managements.Include(m => m.Department);
            return View(await adminDbContext.ToListAsync());
        }

        // GET: Managements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var management = await _context.Managements
                .Include(m => m.Department)
                .FirstOrDefaultAsync(m => m.IdManagement == id);
            if (management == null)
            {
                return NotFound();
            }

            return View(management);
        }

        // GET: Managements/Create
        public IActionResult Create()
        {
            //ViewData["DepartmentId"] = new SelectList(_context.Departments.Where(x=>x.IsActive), "IdDepartment", "IdDepartment");
            List<Department> departments= _context.Departments
                              .Where(x => x.IsActive).ToList();
            return View(new ManagementDTO() { Departments = departments});
        }

        // POST: Managements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagementDTO model, IFormFile Logo)
        {
            ModelState.Remove("management.Logo");
            ModelState.Remove("management.Department");
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Logo.CopyToAsync(memoryStream);
                    model.management.Logo = memoryStream.ToArray();
                }
                _context.Add(model.management);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment", model.management.DepartmentId);
            return View(model);
        }

        // GET: Managements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var management = await _context.Managements.FindAsync(id);
            if (management == null)
            {
                return NotFound();
            }
            List<Department> departments = _context.Departments
                              .Where(x => x.IsActive).ToList();
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment", management.DepartmentId);
            return View(new ManagementDTO() { Departments = departments,management=management });
        }

        // POST: Managements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ManagementDTO model, IFormFile Logo)
        {
            if (id != model.management.IdManagement)
            {
                return NotFound();
            }
            ModelState.Remove("management.Logo");
            ModelState.Remove("management.Department");
            if (ModelState.IsValid)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Logo.CopyToAsync(memoryStream);
                        model.management.Logo = memoryStream.ToArray();
                    }
                    _context.Update(model.management);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagementExists(model.management.IdManagement))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "IdDepartment", "IdDepartment", model.management.DepartmentId);
            return View(model);
        }

        // GET: Managements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var management = await _context.Managements
                .Include(m => m.Department)
                .FirstOrDefaultAsync(m => m.IdManagement == id);
            if (management == null)
            {
                return NotFound();
            }

            return View(management);
        }

        // POST: Managements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var management = await _context.Managements.FindAsync(id);
            if (management != null)
            {
                _context.Managements.Remove(management);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagementExists(int id)
        {
            return _context.Managements.Any(e => e.IdManagement == id);
        }
    }
}
