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
    public class UserProfessionsController : Controller
    {
        private readonly LPMContext _context;

        public UserProfessionsController(LPMContext context)
        {
            _context = context;
        }

        // GET: UserProfessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserProfession.ToListAsync());
        }

        // GET: UserProfessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfession = await _context.UserProfession
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfession == null)
            {
                return NotFound();
            }

            return View(userProfession);
        }

        // GET: UserProfessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserProfessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Education,Profession,Workplace,AnnualIncome")] UserProfession userProfession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProfession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userProfession);
        }

        // GET: UserProfessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfession = await _context.UserProfession.FindAsync(id);
            if (userProfession == null)
            {
                return NotFound();
            }
            return View(userProfession);
        }

        // POST: UserProfessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Education,Profession,Workplace,AnnualIncome")] UserProfession userProfession)
        {
            if (id != userProfession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfessionExists(userProfession.Id))
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
            return View(userProfession);
        }

        // GET: UserProfessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfession = await _context.UserProfession
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfession == null)
            {
                return NotFound();
            }

            return View(userProfession);
        }

        // POST: UserProfessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfession = await _context.UserProfession.FindAsync(id);
            _context.UserProfession.Remove(userProfession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfessionExists(int id)
        {
            return _context.UserProfession.Any(e => e.Id == id);
        }
    }
}
