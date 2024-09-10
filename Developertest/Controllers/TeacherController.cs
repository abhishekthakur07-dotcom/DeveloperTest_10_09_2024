using Microsoft.AspNetCore.Mvc;
using Developertest.Models;
using Developertest.Repos;

namespace Developertest.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeacherRepository _repository;

        public TeacherController(TeacherRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

  
        [HttpPost]
        public IActionResult Create(Teacher teacher, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }
                    teacher.ImagePath = "/images/" + imageFile.FileName; // Save relative path to DB
                }
                else
                {
                    teacher.ImagePath = "/images/default.png"; // Or handle no image scenario
                }

                _repository.AddTeacher(teacher);
                return RedirectToAction("Index");
            }

            return View(teacher);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var teachers = _repository.GetAllTeachers();
            return View(teachers);
        }
    }
}
