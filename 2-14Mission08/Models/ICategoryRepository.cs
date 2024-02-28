using System.Diagnostics;

namespace _2_14Mission08.Models
{
    public interface ICategoryRepository
    {
        List<Category> Categories { get; }
        IQueryable<Task> Tasks { get; }
        Task GetTaskById(int id);

    }
}
