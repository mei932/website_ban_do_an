using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWNC.Areas.Admin.Controllers
{

    public class HomeController : Controller
    {
        [Area("admin")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
