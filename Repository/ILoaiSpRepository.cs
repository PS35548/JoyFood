using ps35548_asm.Models;
namespace ps35548_asm.Repository
{
    public interface ILoaiSpRepository
    {
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(String categoryId); 
        Category GetLoaiSp(String categoryId);
        IEnumerable<Category> GetAllLoaiSpS();
    }
}
