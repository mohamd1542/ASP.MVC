using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsMarkWithExcel.Models;

namespace StudentsMarkWithExcel.Controllers
{
    public class GroupStudentsController : Controller
    {
        private readonly StudentsMarkLiveContext _context;

        public GroupStudentsController(StudentsMarkLiveContext context)
        {
            _context = context;
        }

        // GET: GroupStudents
        public async Task<IActionResult> Index()
        {
            var studentsMarkLiveContext = _context.GroupStudent.Include(g => g.Group).Include(g => g.Student);
            return View(await studentsMarkLiveContext.ToListAsync());
        }

        // GET: GroupStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GroupStudent == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudent
                .Include(g => g.Group)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupStudent == null)
            {
                return NotFound();
            }

            return View(groupStudent);
        }

        // GET: GroupStudents/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentName");
            return View();
        }

        // POST: GroupStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,GroupId")] GroupStudent groupStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Id", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", groupStudent.StudentId);
            return View(groupStudent);
        }

        // GET: GroupStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GroupStudent == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudent.FindAsync(id);
            if (groupStudent == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentName", groupStudent.StudentId);
            return View(groupStudent);
        }

        // POST: GroupStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,GroupId")] GroupStudent groupStudent)
        {
            if (id != groupStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupStudentExists(groupStudent.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Id", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", groupStudent.StudentId);
            return View(groupStudent);
        }

        // GET: GroupStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GroupStudent == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudent
                .Include(g => g.Group)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupStudent == null)
            {
                return NotFound();
            }

            return View(groupStudent);
        }

        // POST: GroupStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GroupStudent == null)
            {
                return Problem("Entity set 'StudentsMarkLiveContext.GroupStudent'  is null.");
            }
            var groupStudent = await _context.GroupStudent.FindAsync(id);
            if (groupStudent != null)
            {
                _context.GroupStudent.Remove(groupStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupStudentExists(int id)
        {
          return (_context.GroupStudent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
