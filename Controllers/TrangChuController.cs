using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ps35548_asm.Models;
using X.PagedList;

namespace ps35548_asm.Controllers
{
    public class TrangChuController : Controller
    {
        QlbDtContext db = new QlbDtContext();
        private readonly ILogger<TrangChuController> _logger;
        public TrangChuController(ILogger<TrangChuController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0?1:page.Value;
            var lstsanpham = db.Products.AsNoTracking().OrderBy(x=>x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);


            return View(lst);
        }
    }
}
