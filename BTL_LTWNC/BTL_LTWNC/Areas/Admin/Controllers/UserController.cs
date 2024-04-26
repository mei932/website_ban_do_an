using BTL_LTWNC.Data;
using BTL_LTWNC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWNC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/User")]
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("QuanLyNguoiDung")]
        public async Task<IActionResult> Index()
        {
            var data = await _dbContext.userViewModels.FromSqlRaw("spGetUserVM").ToListAsync();

            return View(data);
        }

        [HttpGet]
        [Route("ThemNguoiDung")]
        public IActionResult Create()
        {
            ViewBag.roleList = new SelectList(_dbContext.tblRole.ToList(), "iRoleID", "sRoleName");

            return View();
        }

        [HttpPost]
        [Route("ThemNguoiDung")]
        public async Task<IActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    sUserName = model.sUserName?.Trim(),
                    sUserPassword = model.sUserPassword?.Trim(),
                    iUserRoleID = model.iUserRoleID
                };

                await _dbContext.tblUser.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("QuanLyNguoiDung");
            }

            return View();
        }

        [HttpGet]
        [Route("ChinhSuaNguoiDung")]
        public async Task<IActionResult> Edit(int id)
        {

            ViewBag.roleList = new SelectList(_dbContext.tblRole.ToList(), "iRoleID", "sRoleName");

            var user = await _dbContext.tblUser.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [Route("ChinhSuaNguoiDung")]
        public async Task<IActionResult> Edit(User model)
        {
            var user = await _dbContext.tblUser.FindAsync(model.iUserID);

            if (user != null)
            {
                user.iUserID = model.iUserID;
                user.sUserName = model.sUserName;
                user.sUserPassword = model.sUserPassword;
                user.iUserRoleID = model.iUserRoleID;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("QuanLyNguoiDung");
            }

            return View();
        }

        [HttpGet]
        [Route("XoaNguoiDung")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var user = await _dbContext.tblUser.AsNoTracking()
                .FirstOrDefaultAsync(x => x.iUserID == id);

            var role = await _dbContext.tblRole.SingleOrDefaultAsync(x => x.iRoleID == user.iUserRoleID);

            ViewBag.roleName = role.sRoleName;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [Route("XoaNguoiDung")]
        public async Task<IActionResult> Delete(User model)
        {
            var user = await _dbContext.tblUser.FindAsync(model.iUserID);

            if (user != null)
            {
                _dbContext.tblUser.Remove(user);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("QuanLyNguoiDung");
        }
    }
}
