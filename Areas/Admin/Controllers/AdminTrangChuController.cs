using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ps35548_asm.Models;
using X.PagedList;

namespace ps35548_asm.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class AdminTrangChuController : Controller
    {
        QlbDtContext db = new QlbDtContext();

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 15;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [HttpGet]
        [Route("danhmuchoadon")]
        public IActionResult DanhMucHoaDon(int? page)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1;
            var lstHoaDon = db.Orders.AsNoTracking().OrderByDescending(x => x.OrderDate);
            PagedList<Order> lst = new PagedList<Order>(lstHoaDon, pageNumber, pageSize);

            return View(lst);
        }


        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        [Route("ThemSanPhamMoi")]
        [HttpPost]
        public IActionResult ThemSanPhamMoi(Product sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName", sanPham.CategoryId);
            return View(sanPham);
        }

        [Route("ChiTietSanPham/{id}")]
        public IActionResult ChiTietSanPham(int id)
        {
            var sanPham = db.Products.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        [Route("SuaSanPham/{id}")]
        [HttpGet]
        public IActionResult SuaSanPham(int id)
        {
            var sanPham = db.Products.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName", sanPham.CategoryId);
            return View(sanPham);
        }

        [Route("SuaSanPham/{id}")]
        [HttpPost]
        public IActionResult SuaSanPham(int id, Product sanPham)
        {
            if (id != sanPham.ProductId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DanhMucSanPham");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(sanPham.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName", sanPham.CategoryId);
            return View(sanPham);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Any(e => e.ProductId == id);
        }


        [Route("XoaSanPham/{id}")]
        [HttpGet]
        public IActionResult XoaSanPham(int id)
        {
            var sanPham = db.Products.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        [Route("XoaSanPham/{id}")]
        [HttpPost, ActionName("XoaSanPham")]
        public IActionResult XoaSanPhamConfirmed(int id)
        {
            var sanPham = db.Products.Find(id);
            db.Products.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("DanhMucSanPham");
        }
    }
}
