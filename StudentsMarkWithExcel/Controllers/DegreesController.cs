using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsMarkWithExcel.Models;

namespace StudentsMarkWithExcel.Controllers
{
    public class DegreesController : Controller
    {
        private readonly StudentsMarkLiveContext _context;

        public DegreesController(StudentsMarkLiveContext context)
        {
            _context = context;
        }

        // GET: Degrees
        public async Task<IActionResult> Index()
        {
            var studentsMarkLiveContext = _context.Degrees.Include(d => d.Course).Include(d => d.Student);
            return View(await studentsMarkLiveContext.ToListAsync());
        }

        public ActionResult SearchCourse(string searchTerm)
        {
            var degrees = _context.Degrees.Where(p => p.Course.CourseName.Contains(searchTerm)).Include(p => p.Course);
            _context.Degrees.Where(p => p.Student.StudentName.Contains(searchTerm)).Include(p => p.Student);
            return View(degrees);
        }


        // GET: Degrees/Details/5 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Degrees == null)
            {
                return NotFound();
            }
            
            var degree = await _context.Degrees
                .Include(d => d.Course)
                .Include(d => d.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (degree == null)
            {
                return NotFound();
            }

            return View(degree);
        }

        // GET: Degrees/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentName");
            return View();
        }

        // POST: Degrees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,Mark")] Degree degree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", degree.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", degree.StudentId);
            return View(degree);
        }

        // GET: Degrees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Degrees == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees.FindAsync(id);
            if (degree == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName", degree.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "StudentName", degree.StudentId);
            return View(degree);
        }

        // POST: Degrees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,Mark")] Degree degree)
        {
            if (id != degree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeExists(degree.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", degree.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", degree.StudentId);
            return View(degree);
        }

        // GET: Degrees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Degrees == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees
                .Include(d => d.Course)
                .Include(d => d.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (degree == null)
            {
                return NotFound();
            }

            return View(degree);
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Degrees == null)
            {
                return Problem("Entity set 'StudentsMarkLiveContext.Degrees'  is null.");
            }
            var degree = await _context.Degrees.FindAsync(id);
            if (degree != null)
            {
                _context.Degrees.Remove(degree);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeExists(int id)
        {
          return _context.Degrees.Any(e => e.Id == id);
        }
    }
}
