using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LPM.Web.Context;
using LPM.Web.Models;

namespace LPM.Web.Controllers
{
    public class UserExpectationsController : Controller
    {
        private readonly LPMContext _context;

        public UserExpectationsController(LPMContext context)
        {
            _context = context;
        }

        // GET: UserExpectations
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserExpectation.ToListAsync());
        }

        // GET: UserExpectations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExpectation = await _context.UserExpectation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExpectation == null)
            {
                return NotFound();
            }

            return View(userExpectation);
        }

        // GET: UserExpectations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserExpectations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Education,ColorComplexion,Height")] UserExpectation userExpectation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userExpectation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userExpectation);
        }

        // GET: UserExpectations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExpectation = await _context.UserExpectation.FindAsync(id);
            if (userExpectation == null)
            {
                return NotFound();
            }
            return View(userExpectation);
        }

        // POST: UserExpectations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Education,ColorComplexion,Height")] UserExpectation userExpectation)
        {
            if (id != userExpectation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userExpectation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExpectationExists(userExpectation.Id))
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
            return View(userExpectation);
        }

        // GET: UserExpectations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExpectation = await _context.UserExpectation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExpectation == null)
            {
                return NotFound();
            }

            return View(userExpectation);
        }

        // POST: UserExpectations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userExpectation = await _context.UserExpectation.FindAsync(id);
            _context.UserExpectation.Remove(userExpectation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExpectationExists(int id)
        {
            return _context.UserExpectation.Any(e => e.Id == id);
        }
    }
}
