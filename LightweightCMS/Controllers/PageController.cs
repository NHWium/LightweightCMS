using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LightweightCMS.Data;
using LightweightCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace LightweightCMS.Controllers
{
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user;

        public PageController([FromServices]ApplicationDbContext context, [FromServices] UserManager<IdentityUser> user)
        {
            _context = context;
            _user = user;
        }

        // GET: Page/5
        public IActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(List));
            else
                return RedirectToAction(nameof(View), new { id });
        }

        // GET: Page/List
        public async Task<IActionResult> List()
        {
            IdentityUser currentUser = await GetCurrentUserAsync();
            return View("List", await _context.Page
                .Where(p => (p.Public || (currentUser != null && p.User == currentUser)))
                .Include(p => p.Elements)
                .ToListAsync());
        }

        // GET: Page/View/5
        public async Task<IActionResult> View(int? id)
        {
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (id == null)
            {
                return RedirectToAction(nameof(List));
            }
            var page = await _context.Page.FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }
            //Eager loading
            await _context.Entry(page).Collection(p => p.Elements).LoadAsync();
            // Make sure the user can only touch their own pages
            if (page.User != currentUser && !page.Public)
            {
                return Unauthorized();
            }

            return View(page);
        }

        // GET: Page/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Page/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Titel,Background,Public,Rows,Columns,Gap")] Page page)
        {
            // User should not be sent by the View, to avoid overposting security risk.
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (page != null)
            {
                page.User = currentUser;
                page.Elements = new List<Element>();
                ModelState.Clear();
                if (!TryValidateModel(page))
                {
                    ModelState.AddModelError("User", "Could not add current user.");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Page.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            else await Console.Error.WriteLineAsync("### ERROR ### Page Invalid " + page.ToString() + "\n" + ModelState.ToString());
            return View(page);
        }

        // GET: Page/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            //Eager loading
            await _context.Entry(page).Collection(p => p.Elements).LoadAsync();
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (page.User != currentUser)
            {
                return Unauthorized();
            }
            return View(page);
        }

        // POST: Page/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Titel,Background,Public,Rows,Columns,Gap")] Page page)
        {
            if (page == null)
            {
                return NotFound();
            }
            // User should not be sent by the View, to avoid overposting security risk.
            IdentityUser currentUser = await GetCurrentUserAsync();
            page.User = currentUser;
            page.PageId = id;
            ModelState.Clear();
            if (!TryValidateModel(page))
            {
                ModelState.AddModelError("User", "Could not add current user.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageId))
                    {
                        return StatusCode(500);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(page);
        }

        // GET: Page/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }
            //Eager loading
            await _context.Entry(page).Collection(p => p.Elements).LoadAsync();
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (page.User != currentUser)
            {
                return Unauthorized();
            }

            return View(page);
        }

        // POST: Page/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _context.Page.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            //Eager loading
            await _context.Entry(page).Collection(p => p.Elements).LoadAsync();
            _context.Page.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        // GET: Page/AddElement/5
        public async Task<IActionResult> AddElement(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            //Eager loading
            await _context.Entry(page).Collection(p => p.Elements).LoadAsync();
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (page.User != currentUser)
            {
                return Unauthorized();
            }
            Element element = new Element();
            element.PageId = page.PageId;
            page.Elements.Add(element);
            _context.Page.Update(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id });
        }

        private bool PageExists(int id)
        {
            return _context.Page.Any(e => e.PageId == id);
        }

        //Get user from current context
        private async Task<IdentityUser> GetCurrentUserAsync()
        {
            return await _user.GetUserAsync(HttpContext.User);
        }

    }
}
