using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ps35548_asm.Models;

namespace ps35548_asm.Controllers
{
    public class LoginController : Controller
    {
        QlbDtContext db = new QlbDtContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "TrangChu");
            }

        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName);
                    return RedirectToAction("Index", "TrangChu");
                }
            }
            // Nếu đăng nhập không thành công, chuyển hướng lại đến trang đăng nhập
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Login");
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using ps35548_asm.Models;

//namespace ps35548_asm.Controllers
//{
//    public class LoginController : Controller
//    {
//        private readonly QlbDtContext db = new QlbDtContext();

//        public IActionResult Index()
//        {
//            if (HttpContext.Session.GetString("UserName") == null)
//            {
//                return View();
//            }
//            else
//            {
//                return RedirectToAction("Index", "TrangChu");
//            }
//        }

//        [HttpPost]
//        public IActionResult Login(User user)
//        {
//            if (HttpContext.Session.GetString("UserName") == null)
//            {
//                var u = db.Users
//                    .Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password))
//                    .FirstOrDefault();

//                if (u != null)
//                {
//                    HttpContext.Session.SetString("UserName", u.UserName);
//                    return RedirectToAction("Index", "TrangChu");
//                }
//                else
//                {
//                    // Thêm thông báo vào ViewBag để thông báo lỗi đăng nhập
//                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
//                    return View("Index"); // Chuyển hướng về trang đăng nhập kèm thông báo lỗi
//                }
//            }
//            return RedirectToAction("Index", "TrangChu");
//        }
//    }
//}
