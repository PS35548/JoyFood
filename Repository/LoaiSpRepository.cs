using NuGet.Protocol;
using ps35548_asm.Models;

namespace ps35548_asm.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QlbDtContext _context;
        public LoaiSpRepository(QlbDtContext context)
        {
            _context = context;
        }
        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(string categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllLoaiSpS()
        {
            return _context.Categories;
        }

        public Category GetLoaiSp(string categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public Category Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
