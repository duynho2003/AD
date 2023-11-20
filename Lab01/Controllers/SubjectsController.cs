using Lab01.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab01.Controllers
{
    public class SubjectsController : Controller
    {
        AdddbContext _db = new AdddbContext();
        public IActionResult Index(string? sudentCode)
        {
            var model = _db.Marks.Where(s => s.StudentCode!.Equals(sudentCode)).ToList();
            if (sudentCode != null)
            {
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Mark newSubject)
        {
            try
            {
                var model = _db.Students.FirstOrDefault(s => s.StudentCode.Equals(newSubject.StudentCode));
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        _db.Marks.Add(newSubject);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.msg = "The student hasn't found!";
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
        
        [HttpGet]
        public IActionResult UpdateMark(string studentCode, string subject)
        {
            var model = _db.Marks.FirstOrDefault(m=>m.StudentCode!.Equals(studentCode) && 
                                                                    m.Subject.Equals(subject));
            return View();
        }

        [HttpPost]
        public IActionResult UpdateMark(string studentCode, Mark editSubject)
        {
            try
            {
                var model = _db.Marks.FirstOrDefault(m => m.StudentCode!.Equals(studentCode) && 
                                                            m.Subject.Equals(editSubject.Subject));
                if (model != null)
                {
                    model.Mark1= editSubject.Mark1;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "The student not exist...!";
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Faill");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteSubject(string subject)
        {
            var model = _db.Marks.FirstOrDefault(s => s.Subject.Equals(subject));
            if (model != null) 
            {
                _db.Marks.Remove(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
