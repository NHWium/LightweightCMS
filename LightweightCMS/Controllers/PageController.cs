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

namespace LightweightCMS.Controllers
{
    [Authorize]
    public class PageController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _user;

        public PageController([FromServices]ApplicationDbContext context, [FromServices] UserManager<IdentityUser> user)
        {
            _context = context;
            _user = user;
        }


        // GET: Page
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pages
                .Where(p => (p.User == _user.GetUserAsync(User).Result))
                .ToListAsync());
        }

        // GET: Page/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            // Make sure the user can only touch their own pages
            if (page.User != await _user.GetUserAsync(User))
            {
                return Unauthorized();
            }

            return View(page);
        }

        // GET: Page/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Page/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titel,Background,Public,Rows,Counts,Gap")] Page page)
        {
            /**
             * User should not be sent by the View, to avoid overposting security risk.
             */
            if (page != null)
            {
                page.User = await _user.GetUserAsync(User);
                ModelState.Clear();
                if (!TryValidateModel(page))
                {
                    ModelState.AddModelError("User", "Could not add current user.");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Pages.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else await Console.Error.WriteLineAsync("### ERROR ### Page Invalid " + page.ToString() + "\n" + ModelState.ToString());
            return View(page);
        }

        // GET: Page/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            if (page.User != await _user.GetUserAsync(User))
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
        public async Task<IActionResult> Edit(int id, [Bind("PageId,Titel,Background,Public,Rows,Counts,Gap")] Page page)
        {
            if (id != page.PageId)
            {
                return NotFound();
            }
            /**
             * User should not be sent by the View, to avoid overposting security risk.
             */
            if (page != null)
            {
                page.User = await _user.GetUserAsync(User);
                ModelState.Clear();
                if (!TryValidateModel(page))
                {
                    ModelState.AddModelError("User", "Could not add current user.");
                }
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
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Page/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }
            if (page.User != await _user.GetUserAsync(User))
            {
                return Unauthorized();
            }

            return View(page);
        }

        // POST: Page/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.PageId == id);
        }
    }
}
