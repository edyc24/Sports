using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sports.Data;
using Sports.Models;
using System.Data;

namespace Sports.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiscussionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Discussions
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var discussions = _context.Discussions
                .Include(d => d.Category)
                .Include(d => d.Author);
            return View(await discussions.ToListAsync());
        }

        // GET: Discussions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussions
                .Include(d => d.Category)
                .Include(d => d.Author)
                .Include(d => d.Comments)
                .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GET: Discussions/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CategoryId")] Discussion discussion)
        {
                var user = await _userManager.GetUserAsync(User);
                discussion.Author = user;
                _context.Add(discussion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", discussion.CategoryId);
            return View(discussion);
        }

        // GET: Discussions/Edit/5
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", discussion.CategoryId);
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CategoryId")] Discussion discussion)
        {
            if (id != discussion.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", discussion.CategoryId);
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussions
                .Include(d => d.Category)
                .Include(d => d.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);
            _context.Discussions.Remove(discussion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussions.Any(e => e.Id == id);
        }
    }
}
