using Microsoft.AspNetCore.Mvc;

namespace Developertest.Controllers
{
    public class MasterController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
