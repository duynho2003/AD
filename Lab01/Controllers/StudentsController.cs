using Lab01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab01.Controllers
{
    public class StudentsController : Controller
    {
        AdddbContext _db = new AdddbContext();
        public IActionResult Index()
        {
            var students = _db.Students.ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student newStudent)
        {
            try
            {
                var model = _db.Students.FirstOrDefault(s => s.StudentCode.Equals(newStudent.StudentCode));
                if (ModelState.IsValid)
                {
                    if (model == null)
                    {
                         _db.Students.Add(newStudent);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.msg = "Student already existed!";
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Fail");
                }
            }
            catch (Exception ex) 
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }
    }
}
