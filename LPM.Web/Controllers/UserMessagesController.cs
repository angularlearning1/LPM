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
    public class UserMessagesController : Controller
    {
        private readonly LPMContext _context;

        public UserMessagesController(LPMContext context)
        {
            _context = context;
        }

        // GET: UserMessages
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserMessage.ToListAsync());
        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // GET: UserMessages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProfileId,Message,SentDate")] UserMessage userMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userMessage);
        }

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessage.FindAsync(id);
            if (userMessage == null)
            {
                return NotFound();
            }
            return View(userMessage);
        }

        // POST: UserMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProfileId,Message,SentDate")] UserMessage userMessage)
        {
            if (id != userMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMessageExists(userMessage.Id))
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
            return View(userMessage);
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // POST: UserMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMessage = await _context.UserMessage.FindAsync(id);
            _context.UserMessage.Remove(userMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMessageExists(int id)
        {
            return _context.UserMessage.Any(e => e.Id == id);
        }
    }
}
