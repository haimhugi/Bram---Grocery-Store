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
                return View("../Users/Login");
            }
            var Context = _context.Cart.Where(cart => cart.UserId == int.Parse(HttpContext.Session.GetString("userId")));
            try
            {
                Context.Count();
                return View(await Context.ToListAsync());
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
                return View("../Users/Login");
            }
            return View();
        }

        // GET: Carts/Details/5
        public IActionResult Load(int? id)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/Login");
            }
            if (id == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("CartId", id.ToString());
            return View("../Products/Index", _context.Product);
        }


        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["TotalCartPrice"] = _context.ProductCart.Where(p => p.CartId == cart.Id).Select(p1 => p1.FinalPrice).Sum();
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
            HttpContext.Session.SetString("CartId", "Deleted");
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
