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
    public class HastumsController : Controller
    {
        private readonly TestContext _context;

        public HastumsController(TestContext context)
        {
            _context = context;
        }

        //GET: Hastums
        public async Task<IActionResult> Index(string SearchString)
        {
            var testContext = _context.Hasta.Include(h => h.IlgilenendoktorNavigation).Include(h => h.IlgilenenhemsireNavigation).Include(h => h.TcnoNavigation);
            if (!String.IsNullOrEmpty(SearchString))
                {
                    testContext = _context.Hasta.Where(s => s.Adsoyad.Contains(SearchString)).Include(h => h.IlgilenendoktorNavigation).Include(h => h.IlgilenenhemsireNavigation).Include(h => h.TcnoNavigation);
                    return View(await testContext.ToListAsync());
                }
            return View(await testContext.ToListAsync());
        }

        // GET: Hastums/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastum = await _context.Hasta
                .Include(h => h.IlgilenendoktorNavigation)
                .Include(h => h.IlgilenenhemsireNavigation)
                .Include(h => h.TcnoNavigation)
                .FirstOrDefaultAsync(m => m.Tcno == id);
            if (hastum == null)
            {
                return NotFound();
            }

            return View(hastum);
        }

        // GET: Hastums/Create
        public IActionResult Create()
        {
            ViewData["Ilgilenendoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad");
            ViewData["Ilgilenenhemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad");
            ViewData["Tcno"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad");
            return View();
        }

        // POST: Hastums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tcno,Adsoyad,Cinsiyet,Dogumtarihi,Meslek,Öncedengeçirdiğihastalıklar,Ilgilenendoktor,Ilgilenenhemsire,Adres")] Hastum hastum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hastum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ilgilenendoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad", hastum.Ilgilenendoktor);
            ViewData["Ilgilenenhemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad", hastum.Ilgilenenhemsire);
            ViewData["Tcno"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hastum.Tcno);
            return View(hastum);
        }

        // GET: Hastums/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastum = await _context.Hasta.FindAsync(id);
            if (hastum == null)
            {
                return NotFound();
            }
            ViewData["Ilgilenendoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad", hastum.Ilgilenendoktor);
            ViewData["Ilgilenenhemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad", hastum.Ilgilenenhemsire);
            ViewData["Tcno"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hastum.Tcno);
            return View(hastum);
        }

        // POST: Hastums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Tcno,Adsoyad,Cinsiyet,Dogumtarihi,Meslek,Öncedengeçirdiğihastalıklar,Ilgilenendoktor,Ilgilenenhemsire,Adres")] Hastum hastum)
        {
            if (id != hastum.Tcno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastumExists(hastum.Tcno))
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
            ViewData["Ilgilenendoktor"] = new SelectList(_context.Doktors, "AdSoyad", "AdSoyad", hastum.Ilgilenendoktor);
            ViewData["Ilgilenenhemsire"] = new SelectList(_context.Hemsires, "AdSoyad", "AdSoyad", hastum.Ilgilenenhemsire);
            ViewData["Tcno"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hastum.Tcno);
            return View(hastum);
        }

        // GET: Hastums/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastum = await _context.Hasta
                .Include(h => h.IlgilenendoktorNavigation)
                .Include(h => h.IlgilenenhemsireNavigation)
                .Include(h => h.TcnoNavigation)
                .FirstOrDefaultAsync(m => m.Tcno == id);
            if (hastum == null)
            {
                return NotFound();
            }

            return View(hastum);
        }

        // POST: Hastums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var hastum = await _context.Hasta.FindAsync(id);
            _context.Hasta.Remove(hastum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastumExists(long id)
        {
            return _context.Hasta.Any(e => e.Tcno == id);
        }
    }
}
