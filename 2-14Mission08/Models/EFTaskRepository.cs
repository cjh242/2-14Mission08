namespace _2_14Mission08.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private DbContext _context;
        public EFTaskRepository(DbContext temp) 
        { 
            _context = temp;
        }

        public List<TaskList> Tasks => _context.Tasks.ToList();
    }
}
