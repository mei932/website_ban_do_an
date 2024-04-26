using BTL_LTWNC.Data;
using BTL_LTWNC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWNC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/ProductCategory")]
    public class ProductCategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductCategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("QuanLyDanhMuc")]
        public async Task<IActionResult> Index()
        {
            var data = await _dbContext.tblProductCategory.OrderBy(x => x.iCategoryID).ToListAsync();

            return View(data);
        }

        [HttpGet]
        [Route("ThemDanhMuc")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("ThemDanhMuc")]
        public async Task<IActionResult> Create(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var category = new ProductCategory()
                {
                    sCategoryName = model.sCategoryName?.Trim()
                };

                await _dbContext.tblProductCategory.AddAsync(category);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("QuanLyDanhMuc");
            }

            return View();
        }

        [HttpGet]
        [Route("ChinhSuaDanhMuc")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _dbContext.tblProductCategory.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [Route("ChinhSuaDanhMuc")]
        public async Task<IActionResult> Edit(ProductCategory model)
        {
            var category = await _dbContext.tblProductCategory.FindAsync(model.iCategoryID);

            if (category != null)
            {
                category.iCategoryID = model.iCategoryID;
                category.sCategoryName = model.sCategoryName;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("QuanLyDanhMuc");
            }

            return View();
        }

        [HttpGet]
        [Route("XoaDanhMuc")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var category = await _dbContext.tblProductCategory
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.iCategoryID == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [Route("XoaDanhMuc")]
        public async Task<IActionResult> Delete(ProductCategory model)
        {
            var category = await _dbContext.tblProductCategory.FindAsync(model.iCategoryID);

            if (category != null)
            {
                _dbContext.tblProductCategory.Remove(category);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("QuanLyDanhMuc");
        }

    }
}
