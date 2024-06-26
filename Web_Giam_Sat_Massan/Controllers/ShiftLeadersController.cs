using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Giam_Sat_Massan.Data;

namespace Web_Giam_Sat_Massan.Controllers
{
    public class ShiftLeadersController : Controller
    {
        private readonly AppDbContext _context;

        public ShiftLeadersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShiftLeaders
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShiftLeader.ToListAsync());
        }

        // GET: ShiftLeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftLeader = await _context.ShiftLeader
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shiftLeader == null)
            {
                return NotFound();
            }

            return View(shiftLeader);
        }

        // GET: ShiftLeaders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftLeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name")] ShiftLeader shiftLeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftLeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shiftLeader);
        }

        // GET: ShiftLeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftLeader = await _context.ShiftLeader.FindAsync(id);
            if (shiftLeader == null)
            {
                return NotFound();
            }
            return View(shiftLeader);
        }

        // POST: ShiftLeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Name")] ShiftLeader shiftLeader)
        {
            if (id != shiftLeader.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftLeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftLeaderExists(shiftLeader.ID))
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
            return View(shiftLeader);
        }

        // GET: ShiftLeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftLeader = await _context.ShiftLeader
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shiftLeader == null)
            {
                return NotFound();
            }

            return View(shiftLeader);
        }

        // POST: ShiftLeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shiftLeader = await _context.ShiftLeader.FindAsync(id);
            if (shiftLeader != null)
            {
                _context.ShiftLeader.Remove(shiftLeader);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftLeaderExists(int id)
        {
            return _context.ShiftLeader.Any(e => e.ID == id);
        }
    }
}
