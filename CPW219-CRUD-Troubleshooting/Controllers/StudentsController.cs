using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(_context);
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(s, _context);

                ViewData["Message"] = $"{s.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(s);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //get the student by id
            Student? s = StudentDb.GetStudent(_context, id);
            if (s == null)
            {
                return NotFound();
            }
            //show it on web page
            return View(s);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(_context, s);

                TempData["Message"] = $"{s.Name} Has Been Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(s);
        }

        public IActionResult Delete(int id)
        {
            Student s = StudentDb.GetStudent(_context, id);
            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student s = StudentDb.GetStudent(_context, id);

            StudentDb.Delete(_context, s);
            TempData["Message"] = "Student Has Been Deleted!";
            return RedirectToAction("Index");
        }
    }
}
