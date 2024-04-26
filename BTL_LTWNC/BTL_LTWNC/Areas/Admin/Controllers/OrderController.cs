using BTL_LTWNC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWNC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Order")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _dbContext;

        public OrderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("QuanLyDonHang")]
        public async Task<IActionResult> Index()
        {
            var data = await _dbContext.tblOrder.OrderBy(x => x.iOrderID).ToListAsync();

            return View(data);
        }

        [HttpGet]
        [Route("OrderDetail")]
        public async Task<IActionResult> Details(int id)
        {
            if (id.ToString() == "")
            {
                return NotFound();
            }

            var order = await _dbContext.tblOrder.FindAsync(id);

            SqlParameter orderID = new SqlParameter("@id", id);

            var details = await _dbContext.orderDetailViewModels.FromSqlRaw("spGetOrderDetailByOrderID @id", orderID).ToListAsync();

            if (order == null && details == null)
            {
                return NotFound();
            }

            TempData["OrderDetails"] = details;

            return View(order);
        }
    }
}
