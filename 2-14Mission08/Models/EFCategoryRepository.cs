namespace _2_14Mission08.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private TaskContext _context;
        public EFCategoryRepository(TaskContext temp)
        {
            _context = temp;
        }
        public List<Category> Categories => _context.Categories.ToList();

    }
}

