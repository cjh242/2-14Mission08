﻿namespace _2_14Mission08.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private TaskContext _context;
        public EFTaskRepository(TaskContext temp) 
        { 
            _context = temp;
        }

        public List<TaskList> Tasks => _context.TaskLists.ToList();

        public void AddTask(Task task)
        {
            _context.Add(task);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Task updatedinfo)
        {
            _context.Update(updatedinfo);
        }
    }
}
