using Microsoft.AspNetCore.Mvc;
using ps35548_asm.Models;

namespace ps35548_asm.Controllers
{
    public class RegisterController : Controller
    {
        private readonly QlbDtContext _db;

        public RegisterController(QlbDtContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tài khoản đã tồn tại trong cơ sở dữ liệu chưa
                var existingUser = _db.Users.FirstOrDefault(u => u.UserName == newUser.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại, vui lòng chọn một tên khác.");
                    return View("Index", newUser);
                }

                // Thêm tài khoản mới vào cơ sở dữ liệu
                _db.Users.Add(newUser);
                _db.SaveChanges();

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Index", "Login");
            }

            // Nếu dữ liệu không hợp lệ, hiển thị lại form đăng ký với thông báo lỗi
            return View("Index", newUser);
        }
    }
}
