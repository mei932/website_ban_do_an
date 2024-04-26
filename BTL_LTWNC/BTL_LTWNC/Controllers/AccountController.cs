using BTL_LTWNC.Data;
using BTL_LTWNC.Models.EF;
using BTL_LTWNC.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;

namespace BTL_LTWNC.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(User model)
        {
            if (model.sUserPassword != model.sUserRepeatPassword)
            {
                ViewBag.ErrorMessage = "Mật khẩu không khớp!";
                return View(model);
            }
            else
            {
                var userNameExist = _dbContext.tblUser.SingleOrDefault(x => x.sUserName == model.sUserName);

                if (userNameExist == null)
                {
                    var user = new User()
                    {
                        sUserName = model.sUserName?.Trim(),
                        sUserPassword = model.sUserPassword?.Trim(),
                        iUserRoleID = 2,
                    };

                    await _dbContext.tblUser.AddAsync(user);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập này đã được sử dụng";
                    return View(model);
                }
            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = _dbContext.tblUser.Where(x => x.sUserName == model.username && x.sUserPassword == model.password).SingleOrDefault();

            if (user != null)
            {
                if (user.iUserRoleID == 1)
                {
                    return Redirect("/admin/");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu sai";
            }

            return View();
        }
    }
}
