using BTL_LTWNC.Data;
using BTL_LTWNC.Helper;
using BTL_LTWNC.Models.EF;
using BTL_LTWNC.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWNC.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CartController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Cart");

                if (data == null)
                {
                    data = new List<CartItem>();
                }

                return data;
            }
        }

        [HttpGet]
        public JsonResult showCount()
        {
            var myCart = Carts;

            if (myCart != null && myCart.Any())
            {
                return Json(new { quantity = myCart.Count });
            }

            return Json(new { quantity = 0 });
        }

        [HttpGet]
        public JsonResult getTotalCart()
        {
            var myCart = Carts;

            if (myCart != null && myCart.Any())
            {
                return Json(new { subtotal = myCart.Sum(x => x.fTotalPrice) });
            }

            return Json(new { subtotal = 0 });
        }

        [HttpGet]
        [Route("CheckOut")]
        public IActionResult CheckOut()
        {
            var myCart = Carts;

            if (myCart != null && myCart.Any())
            {
                ViewBag.CartItem = myCart;
                ViewData["cartItem"] = myCart;
            }

            return View();
        }

        [HttpPost]
        [Route("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(OrderViewModel req)
        {
            if (ModelState.IsValid)
            {
                var myCart = Carts;

                if (myCart != null)
                {
                    var order = new Order()
                    {
                        sCustomerName = req.sCustomerName,
                        sCustomerPhone = req.sCustomerPhone,
                        sCustomerAddress = req.sCustomerAddress,
                        dOrderDate = DateTime.Now,
                        fTotal = myCart.Sum(x => x.fTotalPrice)
                    };

                    await _dbContext.tblOrder.AddAsync(order);
                    await _dbContext.SaveChangesAsync();

                    var currentOrderId = order.iOrderID;

                    myCart.ForEach(x => _dbContext.tblOrderDetail.Add(new OrderDetail()
                    {
                        iDetailOrderID = currentOrderId,
                        iDetailProductID = x.iProductID,
                        iDetailQuantity = x.iQuantity,
                        fDetailPrice = x.fPrice
                    }));

                    await _dbContext.SaveChangesAsync();

                    myCart.Clear();
                }

                HttpContext.Session.Set("Cart", myCart);


                return RedirectToAction("CheckOutSuccess");
            }

            return View();
        }

        [Route("CheckOutSuccess")]
        public IActionResult CheckOutSuccess()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(Carts);
        }

        [HttpPost]
        public JsonResult AddToCart(int id, int quantity)
        {
            var myCart = Carts;

            var item = myCart.SingleOrDefault(x => x.iProductID == id);

            var code = new { Success = false, code = -1 };

            if (item == null)
            {
                var product = _dbContext.tblProduct.SingleOrDefault(x => x.iProductID == id);

                item = new CartItem
                {
                    iProductID = id,
                    sProductName = product.sProductName,
                    sImageUrl = product.sImageUrl,
                    fPrice = product.fPrice,
                    iQuantity = quantity,
                };

                myCart.Add(item);
            }
            else
            {
                item.iQuantity += quantity;
            }

            HttpContext.Session.Set("Cart", myCart);
            code = new { Success = true, code = 1 };

            return Json(code);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var myCart = Carts;

            var code = new { Success = false, code = -1 };

            if (myCart != null)
            {
                var item = myCart.FirstOrDefault(x => x.iProductID == id);

                if (item != null)
                {
                    myCart.Remove(item);
                    code = new { Success = true, code = 1 };
                }
            }

            HttpContext.Session.Set("Cart", myCart);

            return Json(code);
        }
    }
}
