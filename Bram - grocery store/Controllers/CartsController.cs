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
    public class CartsController : Controller
    {
        private readonly Bram___grocery_storeContext _context;

        public CartsController(Bram___grocery_storeContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../Users/LogIn");
            }
            var bram___grocery_storeContext = _context.Cart.Include(c => c.User).Include(c => c.ProductsCart);
            var carts = _context.Cart.Where(cart => cart.UserId == int.Parse(HttpContext.Session.GetString("userId")));
            try
            {
                carts.Count();
                return View(await carts.ToListAsync());
            }
            catch
            {
                return View("EmptyCarts");
            }
        }
        public IActionResult EmptyCarts()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../Users/LogIn");
            }
            return View();
        }

        // GET: ShoppingCarts/Details/5
        public IActionResult Load(int? id)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/LogIn");
            }
            if (id == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("MyShoppingCartId", id.ToString());
            return View("../Products/Index", _context.Product);
        }


        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../Users/LogIn");
            }

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["TotalCartPrice"] = _context.ProductCart.Where(p => p.Cart == cart).Select(p1 => p1.FinalPrice).Sum();
            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,IsPaid,TotalCartPrice")] Cart cart, Product Product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,IsPaid,TotalCartPrice")] Cart cart)
        {
            if (id != cart.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.UserId))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FirstName", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return View("../Products/Index", _context.Product);
            }

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["TotalCartPrice"] = _context.ProductCart.Where(p => p.Cart == cart).Select(p1 => p1.FinalPrice).Sum();
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return View("../Products/Index", _context.Product);
            }

            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.UserId == id);
        }
    }
}
