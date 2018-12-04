using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalization.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
