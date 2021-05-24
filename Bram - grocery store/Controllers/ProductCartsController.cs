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
                .Where(producInCart1 => producInCart1.CartId == int.Parse(HttpContext.Session.GetString("CartId")));
            try
            {
                if (shopProjectContext.Count() == 0)
                {
                    return View("EmptyCart");
                }
                foreach (var producInCart2 in shopProjectContext)
                {
                    producInCart2.Product = _context.Product.Where(product => product.Id == producInCart2.ProductId).FirstOrDefault();
                }
                return View(await shopProjectContext.ToListAsync());
            }
            catch
            {
                return View("EmptyCart");
            }
        }

        public IActionResult EmptyCart()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/Login");
            }
            return View();
        }

        public IActionResult Create(int? id)
        {
            var productCart = new ProductCart();
            productCart.Product = _context.Product.Where(p => p.Id == id).FirstOrDefault();
            productCart.ProductId = productCart.Product.Id;
            productCart.Amount = 1;
            ViewData["ProductId"] = new SelectList(_context.Product, "Id");
            return View(productCart);
        }
        /*
                 public IActionResult Create(int? id)
        {
            var productInCart = new ProductInCart();
            productInCart.product = _context.Product.Where(p => p.Id == id).FirstOrDefault();
            productInCart.ProductId = productInCart.product.Id;
            productInCart.Amount = 1;
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Desc");
            return View(productInCart);
        }
*/

        // POST: ProductCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CartId,Amount,FinalPrice")] ProductCart productCart)
        {
            productCart.Product = _context.Product.Where(p => p.Id == productCart.ProductId).FirstOrDefault();
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/Login");
            }
            if (ModelState.IsValid)
            {
                var myShoppingCartId = HttpContext.Session.GetString("CartId");
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
                    HttpContext.Session.SetString("CartId", myShoppingCartId);
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id");
            return View(productCart);
        }

        // POST: ProductInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../users/Login");
            }
            var productInCart = await _context.ProductCart.FindAsync(id);
            _context.ProductCart.Remove(productInCart);
            await _context.SaveChangesAsync();

            var shopProjectContext = _context.ProductCart
               .Where(producInCart1 => producInCart1.CartId == int.Parse(HttpContext.Session.GetString("CartId")));
            try
            {
                if (shopProjectContext.Count() != 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            catch { }

            return View("EmptyCart");

        }
        private bool ProductCartExists(int id)
        {
            return _context.ProductCart.Any(e => e.Id == id);
        }
    }
}
