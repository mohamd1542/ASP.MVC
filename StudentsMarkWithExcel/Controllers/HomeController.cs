using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using StudentsMarkWithExcel.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace StudentsMarkWithExcel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public void Import(IFormFile file)
        {
            using (var context = new StudentsMarkLiveContext())
            {
                var liststuodent = new List<Student>();
                var listcourse = new List<Course>();
                var listdegram = new List<Degree>();
                using (var stream = new MemoryStream())
                {
                    file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;  
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;
                        var colcount = worksheet.Dimension.Columns;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            liststuodent.Add(new Student()
                            {
                                StudentName = worksheet.Cells[row, 1].Value.ToString()
                            });
                        }
                        context.Students.AddRange(liststuodent);
                        for (int col = 2; col <= colcount; col++)
                        {
                            listcourse.Add(new Course()
                            {
                                CourseName = worksheet.Cells[1, col].Value.ToString()
                            });
                        }
                        context.Courses.AddRange(listcourse);
                        context.SaveChanges();

                        for (int colex = 2; colex <= colcount; colex++)
                        {
                            Course course = context.Courses.Where(c => c.CourseName == worksheet.Cells[1, colex].Value.ToString()).FirstOrDefault();
                            for (int rowex = 2; rowex <= rowcount; rowex++)
                            {
                                Student student = context.Students.Where(c => c.StudentName == worksheet.Cells[rowex, 1].Value.ToString()).FirstOrDefault();
                                listdegram.Add(new Degree
                                {
                                    StudentId = student.Id,
                                    CourseId = course.Id,
                                    Mark = int.Parse(worksheet.Cells[rowex, colex].Value.ToString()),
                                });
                            }
                        }
                        context.Degrees.AddRange(listdegram);
                    }
                }

                context.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}