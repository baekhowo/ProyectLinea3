using Microsoft.AspNetCore.Mvc;

namespace idkhelp.Models
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
