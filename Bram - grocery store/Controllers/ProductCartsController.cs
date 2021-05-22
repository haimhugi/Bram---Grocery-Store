﻿using System;
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
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../Users/Login");
            }
            var shopProjectContext = _context.ProductCart
                .Where(producInCart1 => producInCart1.CartId == int.Parse(HttpContext.Session.GetString("MyShoppingCartId")));
            try
            {
                if (shopProjectContext.Count() == 0)
                {
                    return View("EmptyShoppingCart");
                }
                foreach (var producInCart2 in shopProjectContext)
                {
                    producInCart2.Product = _context.Product.Where(product => product.Id == producInCart2.ProductId).FirstOrDefault();
                }
                return View(await shopProjectContext.ToListAsync());
            }
            catch
            {
                return View("EmptyShoppingCart");
            }
        }

        public IActionResult EmptyShoppingCart()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/LogIn");
            }
            return View();
        }

        public IActionResult Create(int? id)
        {
            var productCart = new ProductCart();
            productCart.Product = _context.Product.Where(p => p.Id == id).FirstOrDefault();
            productCart.ProductId = productCart.Product.Id;
            productCart.Amount = 1;
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Desc");
            return View(productCart);
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

        // POST: ProductCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CartId,Amount,FinalPrice")] ProductCart productCart)
        {
            productCart.Product = _context.Product.Where(p => p.Id == productCart.ProductId).FirstOrDefault();
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/LogIn");
            }
            if (ModelState.IsValid)
            {
                var myShoppingCartId = HttpContext.Session.GetString("MyShoppingCartId");
                if (myShoppingCartId == null)
                {
                    var myNewShoppingCart = new Cart()
                    {
                        ProductsCart = new System.Collections.ObjectModel.ObservableCollection<ProductCart>(),
                        UserId = int.Parse(HttpContext.Session.GetString("userId")),
                        User = _context.User.Where(u => u.Id == int.Parse(HttpContext.Session.GetString("userId"))).FirstOrDefault()
                    };
                    _context.Add(myNewShoppingCart);
                    await _context.SaveChangesAsync();
                    myShoppingCartId = myNewShoppingCart.Id.ToString();
                    HttpContext.Session.SetString("MyShoppingCartId", myShoppingCartId);
                }

                var newProductInCart = new ProductCart()
                {
                    Amount = productCart.Amount,
                    Product = _context.Product.Where(p => p.Id == productCart.ProductId).FirstOrDefault(),
                    ProductId = productCart.ProductId,
                    FinalPrice = (productCart.Amount * productCart.Product.Price),
                    Cart = _context.Cart.Where(S => S.Id == int.Parse(myShoppingCartId)).FirstOrDefault(),
                    CartId = int.Parse(myShoppingCartId)

                };
                var prodactExsits = _context.ProductCart.Where(p => p.ProductId == productCart.ProductId && p.CartId == int.Parse(myShoppingCartId)).FirstOrDefault();
                if (prodactExsits != null)
                {
                    newProductInCart.Id = prodactExsits.Id;
                    return View("../Products/Index", _context.Product);
                }


                _context.Add(newProductInCart);
                await _context.SaveChangesAsync();
                return View("../Products/Index", _context.Product);
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Desc", productCart.ProductId);
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

        // POST: ProductInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/LogIn");
            }
            var productCart = await _context.ProductCart.FindAsync(id);
            _context.ProductCart.Remove(productCart);
            await _context.SaveChangesAsync();

            var shopProjectContext = _context.ProductCart
               .Where(producInCart1 => producInCart1.CartId == int.Parse(HttpContext.Session.GetString("MyShoppingCartId")));
            try
            {
                if (shopProjectContext.Count() != 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            catch { }

            return View("EmptyShoppingCart");

        }

        private bool ProductCartExists(int id)
        {
            return _context.ProductCart.Any(e => e.Id == id);
        }
    }
}
