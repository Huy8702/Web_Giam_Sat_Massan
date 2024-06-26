﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Giam_Sat_Massan.Data;

namespace Web_Giam_Sat_Massan.Controllers
{
    public class Shift1Controller : Controller
    {
        private readonly AppDbContext _context;

        public Shift1Controller(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Edit()
        {
            var shift1 = await _context.Shift1.FindAsync(1);
            if (shift1 == null)
            {
                return NotFound();
            }
            return View(shift1);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,StartTime,EndTime,ShiftLeaderCode,ShiftLeaderName")] Shift1 shift1)
        {
            if (shift1.ID != 1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Shift1.Any(e => e.ID == 1))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit)); // Redirect về trang chỉnh sửa
            }
            return View(shift1);
        }
    }

}
