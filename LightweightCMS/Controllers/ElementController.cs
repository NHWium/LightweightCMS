using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightweightCMS.Data;
using LightweightCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LightweightCMS.Controllers
{
    [Authorize]
    public class ElementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user;

        public ElementController([FromServices]ApplicationDbContext context, [FromServices] UserManager<IdentityUser> user)
        {
            _context = context;
            _user = user;
        }

        // GET: Element/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Element element = await _context.Element.FindAsync(id);
            if (element == null)
            {
                return NotFound();
            }
            Page page = await _context.Page.FindAsync(element.PageId);
            if (page == null)
            {
                return NotFound();
            }
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (page.User != currentUser)
            {
                return Unauthorized();
            }
            return View(element);
        }

        // POST: Element/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Content,Background,RowStart,RowEnd,ColumnStart,ColumnEnd,PageId")] Element element)
        {
            if (element == null)
            {
                return NotFound();
            }
            Page page = await _context.Page.FindAsync(element.PageId);
            if (page == null)
            {
                return NotFound();
            }
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (page.User != currentUser)
            {
                return Unauthorized();
            }
            element.ElementId = id;
            ModelState.Clear();
            if (!TryValidateModel(element))
            {
                ModelState.AddModelError("ElementId", "Could not add id.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(element);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElementExists(id))
                    {
                        return StatusCode(500);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return RedirectToAction(nameof(Edit), new { id });
            }
            return View(element);
        }

        // GET: Element/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Element element = await _context.Element.FindAsync(id);
            if (element == null)
            {
                return NotFound();
            }
            Page page = await _context.Page.FindAsync(element.PageId);
            if (page == null)
            {
                return NotFound();
            }
            IdentityUser currentUser = await GetCurrentUserAsync();
            if (page.User != currentUser)
            {
                return Unauthorized();
            }
            return View(element);
        }

        // POST: Element/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var element = await _context.Element.FindAsync(id);
            if (element == null)
            {
                return NotFound();
            }
            var page = await _context.Page.FindAsync(element.PageId);
            if (page == null)
            {
                return NotFound();
            }
            int pageId = element.PageId;
            page.Elements.Remove(element);
            _context.Element.Remove(element);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Page", new { id = pageId });
        }

        private bool ElementExists(int id)
        {
            return _context.Element.Any(e => e.ElementId == id);
        }
        
        //Get user from current context
        private async Task<IdentityUser> GetCurrentUserAsync()
        {
            return await _user.GetUserAsync(HttpContext.User);
        }
    }
}