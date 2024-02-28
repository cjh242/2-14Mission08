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
            var tasks = _taskRepo.GetAllTaskIncludingRepos();

            return View(tasks);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            //get the record to delete by id
            var recordToDelete = _taskRepo.Tasks
                .Single(x => x.TaskId == Id);

            ViewBag.Categories = _categoryRepo.Categories
                .OrderBy(x => x.CategoryName).ToList();

            return View("ConfirmDelete", recordToDelete);
        }
        [HttpPost]
        public IActionResult Delete(TaskList item)
        {
            
            _taskRepo.Delete(item);
            _taskRepo.Save();

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
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var recordtoEdit = _context.Tasks
        //        .Single(x => x.TaskId == id);

        //    ViewBag.Categories = _context.Categories.ToList();

        //    return View("MoviesCollection", recordtoEdit);

        //}

        //Update and save the changes to the table 
        //[HttpPost]
        //public IActionResult Edit(MovieCollection updatedinfo)
        //{
        //    _context.Update(updatedinfo);
        //    _context.SaveChanges();

        //    return RedirectToAction("MovieTable");
        //}


    }
}
