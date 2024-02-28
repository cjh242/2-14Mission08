using _2_14Mission08.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2_14Mission08.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskRepository _taskRepo;
        private readonly ICategoryRepository _categoryRepo;

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
            var tasks = _taskRepo.TaskList.Include(x => x.Category)
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
        public IActionResult MatrixForm() //add
        {
            return View();
        }
        [HttpPost]
        public IActionResult MatrixForm(TaskList task) //add
        {
            if (ModelState.IsValid)
            {
                //add the new movie 
                _context.Movies.Add(movie);
                //save changes to the database
                _context.SaveChanges();

                //show the confirmation screen
                return View("Confirmation", movie);
            }
            else
            {
                ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName).ToList();
                return View(movie);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
