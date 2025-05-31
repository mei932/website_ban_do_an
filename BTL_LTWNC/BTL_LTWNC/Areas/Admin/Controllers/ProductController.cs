using BTL_LTWNC.Data;
using BTL_LTWNC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWNC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        IWebHostEnvironment envinroment;

        public ProductController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            envinroment = webHostEnvironment;
        }

        [Route("QuanLySanPham")]
        public async Task<IActionResult> Index()
        {
            var data = await _dbContext.productViewModels.FromSqlRaw("spGetProductVM").ToListAsync();

            return View(data);
        }

        [HttpGet]
        [Route("ThemSanPham")]
        public IActionResult Create()
        {
            ViewBag.cateList = new SelectList(_dbContext.tblProductCategory.ToList(), "iCategoryID", "sCategoryName");

            return View();
        }

        [HttpPost]
        [Route("ThemSanPham")]
        public async Task<IActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadImage(model);

                var product = new Product()
                {
                    sProductName = model.sProductName,
                    iProductCategoryID = model.iProductCategoryID,
                    fPrice = model.fPrice,
                    sImageUrl = uniqueFileName
                };

                await _dbContext.tblProduct.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("QuanLySanPham");
            }

            return View();
        }

        [HttpGet]
        [Route("ChinhSuaSanPham")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.cateList = new SelectList(_dbContext.tblProductCategory.ToList(), "iCategoryID", "sCategoryName");

            var product = await _dbContext.tblProduct.Where(x => x.iProductID == id).SingleOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [Route("ChinhSuaSanPham")]
        public async Task<IActionResult> Edit(Product model)
        {
            var product = await _dbContext.tblProduct.FindAsync(model.iProductID);

            if (product != null)
            {
                string uniqueFileName = string.Empty;

                if (model.ImagePath != null)
                {
                    if (product.sImageUrl != null)
                    {
                        string filePath = Path.Combine(envinroment.WebRootPath, "public/images", product.sImageUrl);

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    uniqueFileName = UploadImage(model);
                }

                product.sProductName = model.sProductName;
                product.iProductCategoryID = model.iProductCategoryID;
                product.fPrice = model.fPrice;
                if (model.ImagePath != null)
                {
                    product.sImageUrl = uniqueFileName;
                }

                _dbContext.tblProduct.Update(product);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("QuanLySanPham");
        }

        [HttpGet]
        [Route("XoaSanPham")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var product = await _dbContext.tblProduct.AsNoTracking()
                .FirstOrDefaultAsync(x => x.iProductID == id);

            var category = await _dbContext.tblProductCategory.SingleOrDefaultAsync(x => x.iCategoryID == product.iProductCategoryID);

            ViewBag.categoryName = category.sCategoryName;

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [Route("XoaSanPham")]
        public async Task<IActionResult> Delete(Product model)
        {
            var product = await _dbContext.tblProduct.FindAsync(model.iProductID);

            if (product != null)
            {
                string deleteFromFolder = Path.Combine(envinroment.WebRootPath, "public/images");
                string currentImg = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, product.sImageUrl);

                if (currentImg != null)
                {
                    if (System.IO.File.Exists(currentImg))
                    {
                        System.IO.File.Delete(currentImg);
                    }
                }

                _dbContext.tblProduct.Remove(product);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("QuanLySanPham");
        }

        private string UploadImage(Product model)
        {
            string uniqueFileName = string.Empty;

            if (model.ImagePath != null)
            {
                string uploadFolder = Path.Combine(envinroment.WebRootPath, "public/images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}

