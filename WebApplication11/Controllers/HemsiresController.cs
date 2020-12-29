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
    public class HemsiresController : Controller
    {
        private readonly TestContext _context;

        public HemsiresController(TestContext context)
        {
            _context = context;
        }

        // GET: Hemsires
        public async Task<IActionResult> Index()
        {
            var testContext = _context.Hemsires.Include(h => h.TCNoNavigation).Include(h => h.ÇalıstığıBrimNavigation);
            return View(await testContext.ToListAsync());
        }

        // GET: Hemsires/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hemsire = await _context.Hemsires
                .Include(h => h.TCNoNavigation)
                .Include(h => h.ÇalıstığıBrimNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (hemsire == null)
            {
                return NotFound();
            }

            return View(hemsire);
        }

        // GET: Hemsires/Create
        public IActionResult Create()
        {
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad");
            ViewData["ÇalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi");
            return View();
        }

        // POST: Hemsires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TCNo,AdSoyad,Sifre,Cinsiyet,DogumTarihi,ÇalıstığıBrim")] Hemsire hemsire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hemsire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hemsire.TCNo);
            ViewData["ÇalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi", hemsire.ÇalıstığıBrim);
            return View(hemsire);
        }

        // GET: Hemsires/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hemsire = await _context.Hemsires.FindAsync(id);
            if (hemsire == null)
            {
                return NotFound();
            }
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hemsire.TCNo);
            ViewData["ÇalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi", hemsire.ÇalıstığıBrim);
            return View(hemsire);
        }

        // POST: Hemsires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("TCNo,AdSoyad,Sifre,Cinsiyet,DogumTarihi,ÇalıstığıBrim")] Hemsire hemsire)
        {
            if (id != hemsire.TCNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hemsire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HemsireExists(hemsire.TCNo))
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
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", hemsire.TCNo);
            ViewData["ÇalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi", hemsire.ÇalıstığıBrim);
            return View(hemsire);
        }

        // GET: Hemsires/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hemsire = await _context.Hemsires
                .Include(h => h.TCNoNavigation)
                .Include(h => h.ÇalıstığıBrimNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (hemsire == null)
            {
                return NotFound();
            }

            return View(hemsire);
        }

        // POST: Hemsires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var hemsire = await _context.Hemsires.FindAsync(id);
            _context.Hemsires.Remove(hemsire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HemsireExists(long id)
        {
            return _context.Hemsires.Any(e => e.TCNo == id);
        }
    }
}
