using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bram___grocery_store.Data;
using Bram___grocery_store.Models;
using Microsoft.AspNetCore.Http;

namespace Bram___grocery_store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Bram___grocery_storeContext _context;

        public ProductsController(Bram___grocery_storeContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());

        }

        // GET: Products/Details/5
        [HttpPost]
        public async Task<IActionResult> Search(string product)

        {
            var products = _context.Product.Where(x => x.Name.Contains(product)).OrderBy(x => x.Name);
            return View("../Products/Index", await products.ToListAsync());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,PhotoUrl")] Product product)
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("Admin"))
            {
                return View("../Products/Index", _context.Product);
            }
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,PhotoUrl,CategoryId")] Product product)
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }   

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
