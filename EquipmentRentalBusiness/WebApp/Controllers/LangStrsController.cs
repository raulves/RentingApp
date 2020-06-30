using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using WebApp.ViewModels.Mappers;

#pragma warning disable 1591
namespace WebApp.Controllers
{
    
    [Authorize(Roles = "admin")]
    public class LangStrsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly LangStrVMMapper _mapper = new LangStrVMMapper();

        public LangStrsController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: LangStrs
        public async Task<IActionResult> Index()
        {
            return View((await _bll.LanguageStrings.GetLanguageStrings()).Select(e => _mapper.Map(e)));
        }

        // GET: LangStrs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _context.LangStrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langStr == null)
            {
                return NotFound();
            }

            return View(langStr);
        }

        // GET: LangStrs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LangStrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] LangStr langStr)
        {
            if (ModelState.IsValid)
            {
                langStr.Id = Guid.NewGuid();
                _context.Add(langStr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(langStr);
        }

        // GET: LangStrs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _context.LangStrs.FindAsync(id);
            if (langStr == null)
            {
                return NotFound();
            }
            return View(langStr);
        }

        // POST: LangStrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] LangStr langStr)
        {
            if (id != langStr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(langStr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LangStrExists(langStr.Id))
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
            return View(langStr);
        }

        // GET: LangStrs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _context.LangStrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langStr == null)
            {
                return NotFound();
            }

            return View(langStr);
        }

        // POST: LangStrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var langStr = await _context.LangStrs.FindAsync(id);
            _context.LangStrs.Remove(langStr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LangStrExists(Guid id)
        {
            return _context.LangStrs.Any(e => e.Id == id);
        }
    }
}
