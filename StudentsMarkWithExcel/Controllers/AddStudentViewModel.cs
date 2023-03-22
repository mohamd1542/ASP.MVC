using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentsMarkWithExcel.Controllers
{
    internal class AddStudentViewModel
    {
        public int GroupId { get; set; }
        public SelectList Students { get; set; }
    }
}