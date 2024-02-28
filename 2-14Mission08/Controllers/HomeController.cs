using _2_14Mission08.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2_14Mission08.Controllers
{
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

        public IActionResult MatrixForm()
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
        public IActionResult MatrixForm(){} //add

        [HttpPost]
        public IActionResult MatrixForm(//Modelname response)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(response); // Add record to the database
                _context.SaveChanges();
                return View("Index", response);
            }
            else
            {
                ViewBag.Categories = _context.Categories.ToList();

                return View(response);
            }
        }

        public IActionResult MovieTable()
        {
            var tasks = _context.Tasks.ToList();

            return View(tasks);

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

        //get the movie id from the database and allow changes to be made
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordtoEdit = _context.Tasks
                .Single(x => x.TaskId == id);

            ViewBag.Categories = _context.Categories.ToList();

            return View("MoviesCollection", recordtoEdit);

        }

        //Update and save the changes to the table 
        [HttpPost]
        public IActionResult Edit(MovieCollection updatedinfo)
        {
            _context.Update(updatedinfo);
            _context.SaveChanges();

            return RedirectToAction("MovieTable");
        }


    }
}
