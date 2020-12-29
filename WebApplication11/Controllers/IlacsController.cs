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
    public class IlacsController : Controller
    {
        private readonly TestContext _context;

        public IlacsController(TestContext context)
        {
            _context = context;
        }

        // GET: Ilacs
        public async Task<IActionResult> Index(string SearchString)
        {
            var testContext = _context.Ilacs.Include(i => i.FirmaIsmiNavigation).Include(i => i.HastaTCNoNavigation);
            if (!String.IsNullOrEmpty(SearchString))
            {
                testContext = _context.Ilacs.Where(s => s.IlacIsmi.Contains(SearchString)).Include(i => i.FirmaIsmiNavigation).Include(i => i.HastaTCNoNavigation); ;
                return View(await testContext.ToListAsync());
            }
            return View(await testContext.ToListAsync());
        }

        // GET: Ilacs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilacs
                .Include(i => i.FirmaIsmiNavigation)
                .Include(i => i.HastaTCNoNavigation)
                .FirstOrDefaultAsync(m => m.IlacIsmi == id);
            if (ilac == null)
            {
                return NotFound();
            }

            return View(ilac);
        }

        // GET: Ilacs/Create
        public IActionResult Create()
        {
            ViewData["FirmaIsmi"] = new SelectList(_context.Tedarikcis, "FirmaIsmi", "FirmaIsmi");
            ViewData["HastaTCNo"] = new SelectList(_context.Tedavis, "HastaTCNo", "IlgilenenDoktor");
            return View();
        }

        // POST: Ilacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IlacIsmi,EtkenIsmi,KullanımAmacı,FirmaIsmi,TedaviIsmi,HastaTCNo")] Ilac ilac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirmaIsmi"] = new SelectList(_context.Tedarikcis, "FirmaIsmi", "FirmaIsmi", ilac.FirmaIsmi);
            ViewData["HastaTCNo"] = new SelectList(_context.Tedavis, "HastaTCNo", "IlgilenenDoktor", ilac.HastaTCNo);
            return View(ilac);
        }

        // GET: Ilacs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilacs.FindAsync(id);
            if (ilac == null)
            {
                return NotFound();
            }
            ViewData["FirmaIsmi"] = new SelectList(_context.Tedarikcis, "FirmaIsmi", "FirmaIsmi", ilac.FirmaIsmi);
            ViewData["HastaTCNo"] = new SelectList(_context.Tedavis, "HastaTCNo", "IlgilenenDoktor", ilac.HastaTCNo);
            return View(ilac);
        }

        // POST: Ilacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IlacIsmi,EtkenIsmi,KullanımAmacı,FirmaIsmi,TedaviIsmi,HastaTCNo")] Ilac ilac)
        {
            if (id != ilac.IlacIsmi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlacExists(ilac.IlacIsmi))
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
            ViewData["FirmaIsmi"] = new SelectList(_context.Tedarikcis, "FirmaIsmi", "FirmaIsmi", ilac.FirmaIsmi);
            ViewData["HastaTCNo"] = new SelectList(_context.Tedavis, "HastaTCNo", "IlgilenenDoktor", ilac.HastaTCNo);
            return View(ilac);
        }

        // GET: Ilacs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilacs
                .Include(i => i.FirmaIsmiNavigation)
                .Include(i => i.HastaTCNoNavigation)
                .FirstOrDefaultAsync(m => m.IlacIsmi == id);
            if (ilac == null)
            {
                return NotFound();
            }

            return View(ilac);
        }

        // POST: Ilacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ilac = await _context.Ilacs.FindAsync(id);
            _context.Ilacs.Remove(ilac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlacExists(string id)
        {
            return _context.Ilacs.Any(e => e.IlacIsmi == id);
        }
    }
}
