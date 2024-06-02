using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Models.Entities;
using RingoMediaTask.Services;
using RingoMediaTask.Services.Email;

namespace RingoMediaTask.Controllers
{
    public class RemindersController : Controller
    {
        private readonly AdminDbContext _context;

        public RemindersController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: Reminders
        public async Task<IActionResult> Index()
        {
            var reminders = await _context.Reminders.ToListAsync();
            return View(reminders);
        }

        // GET: Reminders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders
                .FirstOrDefaultAsync(m => m.IdReminder == id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        // GET: Reminders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reminders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReminderFor,EmailForReminder,ReminderDateTime")] Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reminder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        // GET: Reminders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }

        // POST: Reminders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReminder,ReminderFor,EmailForReminder,ReminderDateTime")] Reminder reminder)
        {
            if (id != reminder.IdReminder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reminder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReminderExists(reminder.IdReminder))
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
            return View(reminder);
        }


        [HttpPut]
        public async Task<IActionResult> StopOrReprocess(int id)
        {
            try
            {
                if (id != 0)
                {
                    var reminder = await _context.Reminders.FirstOrDefaultAsync(x => x.IdReminder == id);
                    if (reminder != null)
                    {
                        if (reminder.IsProcessing == 1)
                        {
                            reminder.IsProcessing = 0;
                        }
                        else if (reminder.IsProcessing == 0 || reminder.IsProcessing==2)
                        {
                            reminder.IsProcessing = 1;
                        }
                        _context.Update(reminder);
                        await _context.SaveChangesAsync();
                        return Json(new { success = true, newStatus = reminder.IsProcessing });
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }
                else 
                {
                    return new EmptyResult();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return new EmptyResult();
            }
        }

        // GET: Reminders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders
                .FirstOrDefaultAsync(m => m.IdReminder == id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        // POST: Reminders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder != null)
            {
                _context.Reminders.Remove(reminder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReminderExists(int id)
        {
            return _context.Reminders.Any(e => e.IdReminder == id);
        }
    }
}
