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
    public class KisisController : Controller
    {
        private readonly TestContext _context;

        public KisisController(TestContext context)
        {
            _context = context;
        }

        // GET: Kisis
        public async Task<IActionResult> Index(string SearchString)
        {
            var testContext = _context.Kisis.Include(k => k.UygulamaIsmiNavigation);
            if (!String.IsNullOrEmpty(SearchString))
            {
                testContext = _context.Kisis.Where(s => s.AdSoyad.Contains(SearchString)).Include(k => k.UygulamaIsmiNavigation);
                return View(await testContext.ToListAsync());
            }
            return View(await testContext.ToListAsync());
        }

        // GET: Kisis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisis
                .Include(k => k.UygulamaIsmiNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // GET: Kisis/Create
        public IActionResult Create()
        {
            ViewData["UygulamaIsmi"] = new SelectList(_context.Uygulamas, "UygulamaIsmi", "UygulamaIsmi");
            return View();
        }

        // POST: Kisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TCNo,AdSoyad,Cinsiyet,DogumTarihi,UygulamaIsmi")] Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UygulamaIsmi"] = new SelectList(_context.Uygulamas, "UygulamaIsmi", "UygulamaIsmi", kisi.UygulamaIsmi);
            return View(kisi);
        }

        // GET: Kisis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisis.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }
            ViewData["UygulamaIsmi"] = new SelectList(_context.Uygulamas, "UygulamaIsmi", "UygulamaIsmi", kisi.UygulamaIsmi);
            return View(kisi);
        }

        // POST: Kisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("TCNo,AdSoyad,Cinsiyet,DogumTarihi,UygulamaIsmi")] Kisi kisi)
        {
            if (id != kisi.TCNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KisiExists(kisi.TCNo))
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
            ViewData["UygulamaIsmi"] = new SelectList(_context.Uygulamas, "UygulamaIsmi", "UygulamaIsmi", kisi.UygulamaIsmi);
            return View(kisi);
        }

        // GET: Kisis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisis
                .Include(k => k.UygulamaIsmiNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // POST: Kisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var kisi = await _context.Kisis.FindAsync(id);
            _context.Kisis.Remove(kisi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KisiExists(long id)
        {
            return _context.Kisis.Any(e => e.TCNo == id);
        }
    }
}
