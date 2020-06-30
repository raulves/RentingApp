#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using WebApp.ViewModels;
using WebApp.ViewModels.Mappers;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin")] 
    public class CategoriesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly CategoryVMMapper _mapper = new CategoryVMMapper();

        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Categories.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound(new MessageDTO($"Category with id {id} not found"));
            }

            return View(_mapper.Map(category));
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CategoryCreateEditViewModel
            {
                ParentCategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), nameof(CategoryBLL.Id),
                    nameof(CategoryBLL.Description))
            };


            return View(vm);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateEditViewModel vm)
        {

            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.Categories.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            
            vm.ParentCategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), 
                nameof(CategoryBLL.Id), nameof(CategoryBLL.Description), 
                vm.ParentCategoryId);
            
            return View(vm);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound(new MessageDTO($"Category with id {id} not found"));
            }
            
            var vm = _mapper.Map(category);
            
            vm.ParentCategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), 
                nameof(CategoryBLL.Id), nameof(CategoryBLL.Description), 
                vm.ParentCategoryId);
            
            return View(vm);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.Categories.ExistsAsync(vm.Id))
            {
                return NotFound(new MessageDTO($"Category with this id {id} not found"));
            }

            if (ModelState.IsValid)
            {
                
                await _bll.Categories.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.ParentCategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), 
                nameof(CategoryBLL.Id), nameof(CategoryBLL.Description), 
                vm.ParentCategoryId);
            return View(vm);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound(new MessageDTO($"Category with id {id} not found"));
            }

            return View(_mapper.Map(category));
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Categories.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
