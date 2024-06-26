using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Giam_Sat_Massan.Data;

namespace Web_Giam_Sat_Massan.Controllers
{
    public class Shift2Controller : Controller
    {
        private readonly AppDbContext _context;

        public Shift2Controller(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Edit()
        {
            var shift2 = await _context.Shift2.FindAsync(1);
            if (shift2 == null)
            {
                return NotFound();
            }
            return View(shift2);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,StartTime,EndTime,ShiftLeaderCode,ShiftLeaderName")] Shift2 shift2)
        {
            if (shift2.ID != 1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Shift2.Any(e => e.ID == 1))
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
            return View(shift2);
        }
    }
}
