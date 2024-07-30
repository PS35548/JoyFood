using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ps35548_asm.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASM_1.Controllers
{
    public class CartController : Controller
    {
        private readonly QlbDtContext _context;

        public CartController(QlbDtContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.CartItems = cartItems;
            return View(cartItems);
        }

        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.CartItems = cartItems;
            return View();
        }

        private int IsExist(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(id))
                    return i;
            }
            return -1;
        }

        public IActionResult Buy(int id)
        {
            Product Product = _context.Products.Find(id);
            if (Product != null)
            {
                List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                int index = IsExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = Product, Quantity = 1 });
                }
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cart != null)
            {
                int index = IsExist(id);
                if (index != -1)
                {
                    cart.RemoveAt(index);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }
            return RedirectToAction("Index");
        }

        public decimal GetTotal()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cart == null)
                return 0;
            return cart.Sum(item => item.Product.Price * item.Quantity);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cart != null)
            {
                int index = IsExist(id);
                if (index != -1)
                {
                    cart[index].Quantity = quantity;
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            // Trả về dữ liệu tổng tiền mới để cập nhật trên giao diện
            return Json(new { total = CalculateTotal(cart) });
        }

        private decimal CalculateTotal(List<CartItem> cart)
        {
            if (cart == null)
                return 0;
            return cart.Sum(item => item.Product.Price * item.Quantity);
        }

        [HttpPost]
        public IActionResult ProcessCheckout(string shippingAddress, string paymentMethod)
        {
            // Lấy thông tin giỏ hàng từ session
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cart == null || !cart.Any())
            {
                // Xử lý trường hợp giỏ hàng rỗng
                return RedirectToAction("Index");
            }

            // Tạo đơn hàng mới
            Order newOrder = new Order
            {
                ShippingAddress = shippingAddress,
                PaymentMethod = paymentMethod,
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                }).ToList(),
                TotalAmount = CalculateTotal(cart)
            };

            // Lưu đơn hàng vào cơ sở dữ liệu
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            // Xóa giỏ hàng sau khi thanh toán
            HttpContext.Session.Remove("Cart");

            // Chuyển hướng đến trang xác nhận đơn hàng hoặc trang cảm ơn
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View("OrderConfirmation");
        }

    }
}
