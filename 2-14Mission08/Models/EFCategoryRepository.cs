using System.Diagnostics;

namespace _2_14Mission08.Models
{
    public class EFCategoryRepository
    {
        private DbContext _context;
        public EFCategoryRepository(DbContext temp)
        {
            _context = temp;
        }

        public List<Category> Categories => _context.Categories.ToList();
    }
}
