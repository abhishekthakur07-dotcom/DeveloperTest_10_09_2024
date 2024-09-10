using Microsoft.AspNetCore.Mvc;
using Developertest.Models;
using Developertest.Repos;

namespace Developertest.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository _repository;

        public StudentController(StudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student, IFormFile imageFile)
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
                    student.ImagePath = "/images/" + imageFile.FileName; // Save relative path to DB
                }
                else
                {
                    student.ImagePath = "/images/default.png"; // handle no image scenario
                }

                _repository.AddStudent(student);
                return RedirectToAction("Index");
            }

        return View(student);
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var students = _repository.GetAllStudents();

            // Filter by name 
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Order students by class numerically
            students = students.OrderBy(s => s.Class).ToList();

            // Populate ViewBag for the search form
            ViewBag.SearchString = searchString;

            return View(students);
        }

    }
}

