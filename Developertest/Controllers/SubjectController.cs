using Microsoft.AspNetCore.Mvc;
using Developertest.Models;
using Developertest.Repos;

namespace Developertest.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectRepository _repository;

        public SubjectController(SubjectRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _repository.AddSubject(subject);
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var subjects = _repository.GetAllSubjects();
            return View(subjects);
        }
    }
}
