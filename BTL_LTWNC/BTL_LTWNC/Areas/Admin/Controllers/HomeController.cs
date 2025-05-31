using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWNC.Areas.Admin.Controllers
{

    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
