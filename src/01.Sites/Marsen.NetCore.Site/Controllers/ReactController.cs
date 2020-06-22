using Microsoft.AspNetCore.Mvc;

namespace Marsen.NetCore.Site.Controllers
{
    public class ReactController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}