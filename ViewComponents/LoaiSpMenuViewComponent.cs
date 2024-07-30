using ps35548_asm.Models;
using Microsoft.AspNetCore.Mvc;
using ps35548_asm.Repository;

namespace ps35548_asm.ViewComponents
{
    public class LoaiSpMenuViewComponent: ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;
        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;
        }
        public IViewComponentResult Innoke() { 
        var loaisp = _loaiSp.GetAllLoaiSpS().OrderBy(x => x.CategoryName);
        return View(loaisp);
                }
    }
}
