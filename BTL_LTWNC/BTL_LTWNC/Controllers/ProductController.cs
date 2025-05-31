using BTL_LTWNC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWNC.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("shop")]
        public async Task<IActionResult> getAllProduct()
        {
            var data = await _dbContext.tblProduct.ToListAsync();

            return View(data);
        }

        [HttpGet]
        [Route("ProductDetails")]
        public async Task<IActionResult> productDetails(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var product = await _dbContext.tblProduct.Where(x => x.iProductID == id).SingleOrDefaultAsync();

            SqlParameter cateID = new SqlParameter("@categoryID", product.iProductCategoryID);

            var relatedProduct = await _dbContext.tblProduct.FromSqlRaw("spGetRelatedProduct @categoryID", cateID).ToListAsync();

            ViewBag.relatedProduct = relatedProduct;

            if (product == null)
            {
                return NotFound();
            }

            // return View(product);
            return View("ProductDetails", product);

        }
    }
}
