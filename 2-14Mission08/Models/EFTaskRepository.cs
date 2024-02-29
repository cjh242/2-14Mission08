using Microsoft.EntityFrameworkCore;

namespace _2_14Mission08.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private TaskContext _context;
        public EFTaskRepository(TaskContext temp) 
        { 
            _context = temp;
        }
        public List<TaskList> Tasks => _context.TaskLists.ToList();
        public void AddTask(TaskList task)
        {
            _context.Add(task);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Delete(TaskList task) 
        {
            _context.Remove(task);
        }

        public List<TaskList> GetTasksIncludingCategories()
        {
            return _context.TaskLists.Include(x => x.Category).OrderBy(x => x.Category).ToList();
        }
    }
}
