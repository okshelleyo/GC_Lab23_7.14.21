using GC_Lab23_7._14._21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GC_Lab23_7._14._21.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieContext _context;
        public MovieController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movie = _context.Movies.ToList();
            return View(movie);
        }

        public IActionResult DeleteMovie(int id)
        {
            var foundMovie = _context.Movies.Find(id);

            if (foundMovie != null)
            {
                _context.Movies.Remove(foundMovie);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public IActionResult MovieForm(int id)
        {
            if (id == 0)
            {
                return View(new Movie());
            } else
            {
                Movie foundMovie = _context.Movies.Find(id);
                return View(foundMovie);

            }

        }

        public IActionResult SaveMovie(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                if (newMovie.Id == 0)
                {
                    _context.Movies.Add(newMovie);
                    _context.SaveChanges();

                }
                else
                {
                    Movie dbMovie = _context.Movies.Find(newMovie.Id);
                    dbMovie.Title = newMovie.Title;
                    dbMovie.Genre = newMovie.Genre;
                    dbMovie.Runtime = newMovie.Runtime;

                    _context.Entry(dbMovie).State = EntityState.Modified;
                    _context.Update(dbMovie);
                    _context.SaveChanges();
                }

            }

            return RedirectToAction("Index");

        }        


    }
}
