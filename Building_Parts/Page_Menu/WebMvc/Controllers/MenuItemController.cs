using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Controllers;

public class MenuItemController : Controller
{
    private readonly WebMvcContext _context;

    public MenuItemController(WebMvcContext context)
    {
        _context = context;
    }

    // GET: MenuItem
    public async Task<IActionResult> Index()
    {
        return View(await _context.MenuItem.ToListAsync());
    }

    // GET: MenuItem/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menuItem = await _context.MenuItem
            .FirstOrDefaultAsync(m => m.Id == id);
        if (menuItem == null)
        {
            return NotFound();
        }

        return View(menuItem);
    }

    // GET: MenuItem/Create
    public IActionResult Create(int parentMenuId)
    {
        Menu menu = _context.Menu.Single(m => m.Id == parentMenuId);
        return View(new MenuItem { ParentMenu = menu });
    }

    // POST: MenuItem/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int parentMenuId, MenuItem menuItem)
    {
        if (ModelState.IsValid)
        {
            Menu menu = _context.Menu.Single<Menu>(m => m.Id == parentMenuId);
            menuItem.ParentMenu = menu;
            _context.MenuItem.Add(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), nameof(Menu), new { Id = parentMenuId });
        }
        return View(menuItem);
    }

    // GET: MenuItem/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menuItem = await _context.MenuItem.FindAsync(id);
        if (menuItem == null)
        {
            return NotFound();
        }
        return View(menuItem);
    }

    // POST: MenuItem/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ActionName,ControllerName,Url,Disable,HasAccess")] MenuItem menuItem)
    {
        if (id != menuItem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(menuItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(menuItem.Id))
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
        return View(menuItem);
    }

    // GET: MenuItem/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menuItem = await _context.MenuItem
            .FirstOrDefaultAsync(m => m.Id == id);
        if (menuItem == null)
        {
            return NotFound();
        }

        return View(menuItem);
    }

    // POST: MenuItem/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var menuItem = await _context.MenuItem.FindAsync(id);
        if (menuItem != null)
        {
            _context.MenuItem.Remove(menuItem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MenuItemExists(int id)
    {
        return _context.MenuItem.Any(e => e.Id == id);
    }
}
