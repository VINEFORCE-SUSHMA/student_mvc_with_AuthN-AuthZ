using Microsoft.AspNetCore.Mvc;
using STUDENT.Filters;
using STUDENT.Models;
using System.Linq;

namespace studentCRUD.Controllers
{
    [AuthorizeUser]
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }
        [AuthorizeUser(Role = "ADMIN")] // Only admin
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeUser(Role = "ADMIN")]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [AuthorizeUser(Role = "ADMIN")]
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        [AuthorizeUser(Role = "ADMIN")]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        [AuthorizeUser(Role = "ADMIN")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [AuthorizeUser(Role = "ADMIN")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

