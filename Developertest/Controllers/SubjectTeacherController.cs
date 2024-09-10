using Microsoft.AspNetCore.Mvc;
using Developertest.Repos;
using Developertest.Models;
using System.Threading.Tasks;

namespace Developertest.Controllers
{
    public class SubjectTeacherController : Controller
    {
        private readonly SubjectTeacherRepository _subjectTeacherRepo;

        public SubjectTeacherController(SubjectTeacherRepository subjectTeacherRepo)
        {
            _subjectTeacherRepo = subjectTeacherRepo;
        }

        public IActionResult Index()
        {
            var subjectsWithTeachers = _subjectTeacherRepo.GetSubjectsWithTeachers();
            return View(subjectsWithTeachers);
        }
    }
}