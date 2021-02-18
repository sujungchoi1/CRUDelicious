using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = _context.Dishes
                .OrderByDescending(d => d.CreatedAt)
                .ToList();
            ViewBag.AllDishes = AllDishes;
            // Console.WriteLine(AllDishes);
            return View();
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost("create")]
        public IActionResult Create(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                _context.Dishes.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index", newDish);            
            }
            else 
            {
                return View("New");
            }
            // _context.Add(newDish);
            // _context.SaveChanges();
            // return RedirectToAction("Index", newDish);
        }

        [HttpGet("{dishId}")]
        public IActionResult Detail(int dishId)
        {
            ViewBag.OneDish = _context.Dishes.FirstOrDefault(id => id.DishId == dishId);
            return View("Detail");
        }

        [HttpGet("edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish oneDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId); // returns one element
            // ViewBag.oneDish = oneDish;
            return View("Update", oneDish);
        }

        [HttpPost("edit/{dishId}")]
        public IActionResult Update(int dishId, Dish EditedDish)
        {
            Dish RetrievedDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            RetrievedDish.Name = EditedDish.Name;
            RetrievedDish.Chef = EditedDish.Chef;
            RetrievedDish.Tastiness = EditedDish.Tastiness;
            RetrievedDish.Calories = EditedDish.Calories;
            RetrievedDish.Description = EditedDish.Description;
            RetrievedDish.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("Detail");
        }

        [HttpGet("delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish RetrievedDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            _context.Dishes.Remove(RetrievedDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // public IActionResult Privacy()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}
