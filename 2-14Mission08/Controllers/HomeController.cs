using _2_14Mission08.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _2_14Mission08.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskRepository _taskRepo;
        private readonly ICategoryRepository _categoryRepo;

        //using the repos in the controller
        public HomeController(ILogger<HomeController> logger, ITaskRepository taskRepo, ICategoryRepository catRepo)
        {
            _logger = logger;
            _taskRepo = taskRepo;
            _categoryRepo = catRepo;
        }
        public IActionResult Index() 
        { 
            return View(); 
        }
        //get for the quadrant view
        [HttpGet]
        public IActionResult Quadrant() 
        {
            //load up the tasks from the repo
            var tasks = _taskRepo.GetTasksIncludingCategories();

            return View(tasks);
        }
        //get for the delete view
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            //get the record to delete by id from the list we have in the repo
            var recordToDelete = _taskRepo.Tasks
                .Single(x => x.TaskId == Id);

            //load up categories into viewbag
            ViewBag.Categories = _categoryRepo.Categories
                .OrderBy(x => x.CategoryName).ToList();

            return View("Delete", recordToDelete);
        }
        //post for the delete route
        [HttpPost]
        public IActionResult Delete(TaskList item)
        {
            //delete 
            _taskRepo.Delete(item);
            //save
            _taskRepo.Save();

            return RedirectToAction("Quadrant");
        }
        // route for the form
        [HttpGet]
        public IActionResult MatrixForm()
        {
            //load up cat in viewbag
            ViewBag.Categories = _categoryRepo.Categories
                .OrderBy(x => x.CategoryName).ToList();

            return View("MatrixForm", new TaskList());
        }
        //add the new tasks
        [HttpPost]
        public IActionResult MatrixForm(TaskList item)
        {
            //check if state is valid and then add the task
            if (ModelState.IsValid)
            {
                _taskRepo.AddTask(item);
                _taskRepo.Save();
                return RedirectToAction("Quadrant");
            }
            else
            {
                ViewBag.Categories = _categoryRepo.Categories.ToList();

                return View(item);
            }
        }
        //get the movie id from the database and allow changes to be made
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var recordToEdit = _taskRepo.Tasks
                .Single(x => x.TaskId == Id);

            ViewBag.Categories = _categoryRepo.Categories.ToList();

            return View("MatrixForm", recordToEdit);
        }
        //Update and save the changes to the table 
        [HttpPost]
        public IActionResult Edit(TaskList updatedinfo)
        {
            _taskRepo.Update(updatedinfo);
            _taskRepo.Save();

            return RedirectToAction("Quadrant");
        }
    }
}
