namespace _2_14Mission08.Models
{
    public interface ITaskRepository
    {
        List<TaskList> Tasks { get; }

        void AddTask(TaskList task);
        void Save();

        void Delete(TaskList task);

        public List<TaskList> GetTasksIncludingCategories();
    }
}
