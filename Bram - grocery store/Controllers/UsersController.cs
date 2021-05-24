using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bram___grocery_store.Data;
using Bram___grocery_store.Models;

namespace Bram___grocery_store.Controllers
{
    public class UsersController : Controller
    {
        private readonly Bram___grocery_storeContext _context;

        public UsersController(Bram___grocery_storeContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserName,Password,IsAdmin,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("userName") != null && HttpContext.Session.GetString("userName").Equals("admin"))
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return View("../Products/Index", _context.Product);
                }
                var answer = _context.User.Where(x => x.UserName == user.UserName);
                if (answer.Count() > 0)
                {
                    ViewData["Error"] = "The username you selected was caught, please choose another name";
                    return View(user);
                }
                else
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("userName", user.UserName);
                    return View("../Products/Index", _context.Product);
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../Products/Index", _context.Product);
            }
            var user = await _context.User.FindAsync(Int32.Parse(HttpContext.Session.GetString("userId")));
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,UserName,Password,IsAdmin,Email")] User user)
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return View("../Products/Index", _context.Product);
            }
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var answer = _context.User.Where(x => x.UserName == user.UserName);
                    if (answer.Count() > 0 && user.UserName != HttpContext.Session.GetString("userName"))
                    {
                        ViewData["Error"] = "The username you selected was caught, please choose another name";
                        return View(user);
                    }
                    else
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                        return View("../Products/Index", _context.Product);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }

            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null || !HttpContext.Session.GetString("userName").Equals("admin"))
            {
                return View("../Products/Index", _context.Product);
            }
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
        public IActionResult Login()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,UserName,Password")] User user)
        {

            var answer = await _context.User.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefaultAsync();
            if (answer != null)
            {
                HttpContext.Session.SetString("userName", answer.UserName);
                HttpContext.Session.SetString("userId", answer.Id.ToString());    
                return Redirect("../Products/Index");
            }
            else
            {
                ViewData["Error"] = "משתמש לא קיים במערכת נא להירשם!";
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("CartId");
            return View("../Products/Index", _context.Product);
        }
    }
}
