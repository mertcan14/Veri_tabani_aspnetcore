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
    public class DoktorsController : Controller
    {
        private readonly TestContext _context;

        public DoktorsController(TestContext context)
        {
            _context = context;
        }

        // GET: Doktors
        public async Task<IActionResult> Index(string SearchString)
        {
            var testContext = _context.Doktors.Include(d => d.CalıstığıBrimNavigation).Include(d => d.IlgilenenYöneticiNavigation).Include(d => d.TCNoNavigation);
            if (!String.IsNullOrEmpty(SearchString))
            {
                testContext = _context.Doktors.Where(s => s.AdSoyad.Contains(SearchString)).Include(d => d.CalıstığıBrimNavigation).Include(d => d.IlgilenenYöneticiNavigation).Include(d => d.TCNoNavigation);
                return View(await testContext.ToListAsync());
            }
            return View(await testContext.ToListAsync());
        }

        // GET: Doktors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktors
                .Include(d => d.CalıstığıBrimNavigation)
                .Include(d => d.IlgilenenYöneticiNavigation)
                .Include(d => d.TCNoNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktors/Create
        public IActionResult Create()
        {
            ViewData["CalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi");
            ViewData["IlgilenenYönetici"] = new SelectList(_context.Yöneticis, "AdSoyad", "AdSoyad");
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad");
            return View();
        }

        // POST: Doktors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TCNo,AdSoyad,Cinsiyet,DogumTarihi,CalıstığıBrim,Sifre,IlgilenenYönetici")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi", doktor.CalıstığıBrim);
            ViewData["IlgilenenYönetici"] = new SelectList(_context.Yöneticis, "AdSoyad", "AdSoyad", doktor.IlgilenenYönetici);
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", doktor.TCNo);
            return View(doktor);
        }

        // GET: Doktors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktors.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }
            ViewData["CalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi", doktor.CalıstığıBrim);
            ViewData["IlgilenenYönetici"] = new SelectList(_context.Yöneticis, "AdSoyad", "AdSoyad", doktor.IlgilenenYönetici);
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", doktor.TCNo);
            return View(doktor);
        }

        // POST: Doktors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("TCNo,AdSoyad,Cinsiyet,DogumTarihi,CalıstığıBrim,Sifre,IlgilenenYönetici")] Doktor doktor)
        {
            if (id != doktor.TCNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorExists(doktor.TCNo))
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
            ViewData["CalıstığıBrim"] = new SelectList(_context.Polikliniks, "BölümIsmi", "BölümIsmi", doktor.CalıstığıBrim);
            ViewData["IlgilenenYönetici"] = new SelectList(_context.Yöneticis, "AdSoyad", "AdSoyad", doktor.IlgilenenYönetici);
            ViewData["TCNo"] = new SelectList(_context.Kisis, "TCNo", "AdSoyad", doktor.TCNo);
            return View(doktor);
        }

        // GET: Doktors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktors
                .Include(d => d.CalıstığıBrimNavigation)
                .Include(d => d.IlgilenenYöneticiNavigation)
                .Include(d => d.TCNoNavigation)
                .FirstOrDefaultAsync(m => m.TCNo == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var doktor = await _context.Doktors.FindAsync(id);
            _context.Doktors.Remove(doktor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(long id)
        {
            return _context.Doktors.Any(e => e.TCNo == id);
        }
    }
}
