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
    public class UserContactsController : BaseController
    {
        private readonly LPMContext _context;

        public UserContactsController(LPMContext context)
        {
            _context = context;
        }

        // GET: UserContacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserContact.Where(x => x.UserId == UserId).ToListAsync());
        }

        

        // GET: UserContacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ContactName,RelationShip,ContactNumber,ContactAddress")] UserContact userContact)
        {
            if (ModelState.IsValid)
            {
                userContact.UserId = UserId;
                _context.Add(userContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userContact);
        }

        // GET: UserContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userContact = await _context.UserContact.Where(x => x.UserId == UserId && x.Id == id).FirstOrDefaultAsync();
            if (userContact == null)
            {
                return NotFound();
            }
            return View(userContact);
        }

        // POST: UserContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ContactName,RelationShip,ContactNumber,ContactAddress")] UserContact userContact)
        {
            if (id != userContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userContact.UserId = UserId;
                    _context.Update(userContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserContactExists(userContact.Id))
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
            return View(userContact);
        }

        // GET: UserContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userContact = await _context.UserContact
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId==UserId);
            if (userContact == null)
            {
                return NotFound();
            }

            return View(userContact);
        }

        // POST: UserContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userContact = await _context.UserContact.FindAsync(id);
            _context.UserContact.Remove(userContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserContactExists(int id)
        {
            return _context.UserContact.Any(e => e.Id == id);
        }
    }
}
