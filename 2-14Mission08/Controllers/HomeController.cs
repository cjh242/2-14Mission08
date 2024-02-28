using _2_14Mission08.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _2_14Mission08.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ITaskRepository _taskRepo;
        private ICategoryRepository _categoryRepo;

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
        [HttpGet]
        public IActionResult Quadrant() 
        {
            var tasks = _taskRepo.Tasks.Include(x => x.Category)
                .OrderBy(x => x.Title).ToList();

            return View(tasks);
        }
        [HttpGet]
        public IActionResult Delete(int Id) 
        {
            //get the record to delete by id
            var recordToDelete = _taskRepo.TaskList
                .Single(x => x.TaskId == Id);

            ViewBag.Categories = _categoryRepo.Categories
                .OrderBy(x => x.CategoryName).ToList();

            return View("ConfirmDelete", recordToDelete);
        }
        [HttpPost]
        public IActionResult Delete(TaskList task)
        {
            //delete the movie and save changes
            _taskRepo.TaskList.Remove(task);
            _taskRepo.SaveChanges();

            return RedirectToAction("ShowMovies");
        }

        [HttpGet]
        public IActionResult MatrixForm()
        {
            ViewBag.Categories = _categoryRepo.Categories
                .OrderBy(x => x.CategoryName).ToList();

            return View("MatrixForm", new TaskList());
        }

        [HttpPost]
        public IActionResult MatrixForm(TaskList item)
        {
            if (ModelState.IsValid)
            {
                _taskRepo.AddTask(item);
                _taskRepo.Save();
                return View("Index");
            }
            else
            {
                ViewBag.Categories = _categoryRepo.Categories.ToList();

                return View(item);
            }
        }
        //get the movie id from the database and allow changes to be made
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordtoEdit = _categoryRepo.GetTaskById(id);
            ViewBag.Categories = _categoryRepo.Categories.ToList();

            return View("MatrixForm", recordtoEdit);

        }

        //Update and save the changes to the table 
        [HttpPost]
        public IActionResult Edit(TaskList updatedinfo)
        {
            _taskRepo.Update(updatedinfo);
            _taskRepo.Save();

            return RedirectToAction("MatrixForm");
        }


    }
}
