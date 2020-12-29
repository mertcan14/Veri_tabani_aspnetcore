using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication11.DataAccess;

namespace WebApplication11.Controllers
{
    public class HastaYakınıController : Controller
    {
        private readonly TestContext _context;

        public HastaYakınıController(TestContext context)
        {
            _context = context;
        }

        // GET: HastaYakını
        public async Task<IActionResult> Index()
        {
            var testContext = _context.HastaYakınıs.Include(h => h.HastaTCNavigation).Include(h => h.TCNoNavigation);
            return View(await testContext.ToListAsync());
        }

        // GET: HastaYakını/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaYakını = await _context.HastaYakınıs
                .Include(h => h.HastaTCNavigation)
                .Include(h => h.TCNoNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (hastaYakını == null)
            {
                return NotFound();
            }

            return View(hastaYakını);
        }

        // GET: HastaYakını/Create
        public IActionResult Create()
        {
            ViewData["HastaTC"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad");
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad");
            return View();
        }

        // POST: HastaYakını/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TCNo,AdSoyad,DogumTarihi,HastaTC")] HastaYakını hastaYakını)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hastaYakını);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaTC"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad", hastaYakını.HastaTC);
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hastaYakını.TCNo);
            return View(hastaYakını);
        }

        // GET: HastaYakını/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaYakını = await _context.HastaYakınıs.FindAsync(id);
            if (hastaYakını == null)
            {
                return NotFound();
            }
            ViewData["HastaTC"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad", hastaYakını.HastaTC);
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hastaYakını.TCNo);
            return View(hastaYakını);
        }

        // POST: HastaYakını/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("TCNo,AdSoyad,DogumTarihi,HastaTC")] HastaYakını hastaYakını)
        {
            if (id != hastaYakını.TCNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastaYakını);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaYakınıExists(hastaYakını.TCNo))
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
            ViewData["HastaTC"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad", hastaYakını.HastaTC);
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hastaYakını.TCNo);
            return View(hastaYakını);
        }

        // GET: HastaYakını/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaYakını = await _context.HastaYakınıs
                .Include(h => h.HastaTCNavigation)
                .Include(h => h.TCNoNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (hastaYakını == null)
            {
                return NotFound();
            }

            return View(hastaYakını);
        }

        // POST: HastaYakını/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var hastaYakını = await _context.HastaYakınıs.FindAsync(id);
            _context.HastaYakınıs.Remove(hastaYakını);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaYakınıExists(long id)
        {
            return _context.HastaYakınıs.Any(e => e.TCNo == id);
        }
    }
}
