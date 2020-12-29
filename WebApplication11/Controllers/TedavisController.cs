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
    public class TedavisController : Controller
    {
        private readonly TestContext _context;

        public TedavisController(TestContext context)
        {
            _context = context;
        }

        // GET: Tedavis
        public async Task<IActionResult> Index()
        {
            var testContext = _context.Tedavis.Include(t => t.HastaTCNoNavigation).Include(t => t.IlgilenenDoktorNavigation).Include(t => t.IlgilenenHemsireNavigation);
            return View(await testContext.ToListAsync());
        }

        // GET: Tedavis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedavi = await _context.Tedavis
                .Include(t => t.HastaTCNoNavigation)
                .Include(t => t.IlgilenenDoktorNavigation)
                .Include(t => t.IlgilenenHemsireNavigation)
                .FirstOrDefaultAsync(m => m.HastaTCNo == id);
            if (tedavi == null)
            {
                return NotFound();
            }

            return View(tedavi);
        }

        // GET: Tedavis/Create
        public IActionResult Create()
        {
            ViewData["HastaTCNo"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad");
            ViewData["IlgilenenDoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad");
            ViewData["IlgilenenHemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad");
            return View();
        }

        // POST: Tedavis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TedaviIsmi,KullanılanIlaç,TedaviSüresi,HastaTCNo,IlgilenenDoktor,IlgilenenHemsire")] Tedavi tedavi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tedavi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaTCNo"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad", tedavi.HastaTCNo);
            ViewData["IlgilenenDoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad", tedavi.IlgilenenDoktor);
            ViewData["IlgilenenHemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad", tedavi.IlgilenenHemsire);
            return View(tedavi);
        }

        // GET: Tedavis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedavi = await _context.Tedavis.FindAsync(id);
            if (tedavi == null)
            {
                return NotFound();
            }
            ViewData["HastaTCNo"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad", tedavi.HastaTCNo);
            ViewData["IlgilenenDoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad", tedavi.IlgilenenDoktor);
            ViewData["IlgilenenHemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad", tedavi.IlgilenenHemsire);
            return View(tedavi);
        }

        // POST: Tedavis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("TedaviIsmi,KullanılanIlaç,TedaviSüresi,HastaTCNo,IlgilenenDoktor,IlgilenenHemsire")] Tedavi tedavi)
        {
            if (id != tedavi.HastaTCNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tedavi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TedaviExists(tedavi.HastaTCNo))
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
            ViewData["HastaTCNo"] = new SelectList(_context.Hasta, "Tcno", "Adsoyad", tedavi.HastaTCNo);
            ViewData["IlgilenenDoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad", tedavi.IlgilenenDoktor);
            ViewData["IlgilenenHemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad", tedavi.IlgilenenHemsire);
            return View(tedavi);
        }

        // GET: Tedavis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedavi = await _context.Tedavis
                .Include(t => t.HastaTCNoNavigation)
                .Include(t => t.IlgilenenDoktorNavigation)
                .Include(t => t.IlgilenenHemsireNavigation)
                .FirstOrDefaultAsync(m => m.HastaTCNo == id);
            if (tedavi == null)
            {
                return NotFound();
            }

            return View(tedavi);
        }

        // POST: Tedavis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tedavi = await _context.Tedavis.FindAsync(id);
            _context.Tedavis.Remove(tedavi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TedaviExists(long id)
        {
            return _context.Tedavis.Any(e => e.HastaTCNo == id);
        }
    }
}
