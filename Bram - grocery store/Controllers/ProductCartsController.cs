using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bram___grocery_store.Data;
using Bram___grocery_store.Models;

namespace Bram___grocery_store.Controllers
{
    public class ProductCartsController : Controller
    {
        private readonly Bram___grocery_storeContext _context;

        public ProductCartsController(Bram___grocery_storeContext context)
        {
            _context = context;
        }

        // GET: ProductCarts
        public async Task<IActionResult> Index()
        {
            var bram___grocery_storeContext = _context.ProductCart.Include(p => p.Cart).Include(p => p.Product);
            return View(await bram___grocery_storeContext.ToListAsync());
        }

        // GET: ProductCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCart = await _context.ProductCart
                .Include(p => p.Cart)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCart == null)
            {
                return NotFound();
            }

            return View(productCart);
        }

        // GET: ProductCarts/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "UserId", "UserId");
            
            //ViewData["UserName"] = new SelectList(_context.User, "UserName", "UserName");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            //ViewData["ProductPrice"] = new SelectList(_context.Product, ViewBag.ProductId, "Price");
            return View();
        }

        // POST: ProductCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CartId,Amount,FinalPrice")] ProductCart productCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "UserId", "UserId", productCart.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productCart.ProductId);
            return View(productCart);
        }

        // GET: ProductCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCart = await _context.ProductCart.FindAsync(id);
            if (productCart == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "UserId", "UserId", productCart.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productCart.ProductId);
            return View(productCart);
        }

        // POST: ProductCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,CartId,Amount,FinalPrice")] ProductCart productCart)
        {
            if (id != productCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCartExists(productCart.Id))
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
            ViewData["CartId"] = new SelectList(_context.Cart, "UserId", "UserId", productCart.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productCart.ProductId);
            return View(productCart);
        }

        // GET: ProductCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCart = await _context.ProductCart
                .Include(p => p.Cart)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCart == null)
            {
                return NotFound();
            }

            return View(productCart);
        }

        // POST: ProductCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCart = await _context.ProductCart.FindAsync(id);
            _context.ProductCart.Remove(productCart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCartExists(int id)
        {
            return _context.ProductCart.Any(e => e.Id == id);
        }
    }
}
