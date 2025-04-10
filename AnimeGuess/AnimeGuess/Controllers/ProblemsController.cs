using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimeGuess.Data;
using AnimeGuess.Models;

namespace AnimeGuess.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly AnimeGuessContext _context;

        public ProblemsController(AnimeGuessContext context)
        {
            _context = context;
        }

        // GET: Problems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Problem.ToListAsync());
        }

        // GET: Problems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // GET: Problems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Answer,Description1,Description2,Description3,ShadowImage,Image,ImageDescription")] Problem problem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problem);
        }

        // GET: Problems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problem.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }
            return View(problem);
        }

        // POST: Problems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Answer,Description1,Description2,Description3,ShadowImage,Image,ImageDescription")] Problem problem)
        {
            if (id != problem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemExists(problem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(problem);
        }

        // GET: Problems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problem = await _context.Problem.FindAsync(id);
            if (problem != null)
            {
                _context.Problem.Remove(problem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Play()
        {
            var number =  _context.Problem.Count();
            Random random = new Random();
            int id = random.Next(1, number+1);
            var problem = await _context.Problem.FindAsync(id);
            return View(problem);
        }

        private bool ProblemExists(int id)
        {
            return _context.Problem.Any(e => e.Id == id);
        }
    }
}
