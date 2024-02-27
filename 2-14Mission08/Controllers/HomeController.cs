using _2_14Mission08.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2_14Mission08.Controllers
{
    {
        //Use the home controller to to navigate through the views and use the move collection to post to the database
        public class HomeController : Controller
        {
            private //Need context file here _context;

            //Set up the views for our website to use

            public HomeController(MoviesCollectionContext temp) //Constructor
            {
                _context = temp;
            }



            public IActionResult Index()
        {
            return View();
        }

        public IActionResult MatrixForm()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Form()
        {
            ViewBag.Categories = _context.Categories.ToList();

            return View("MoviesCollection", new MovieCollection());

        }

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
