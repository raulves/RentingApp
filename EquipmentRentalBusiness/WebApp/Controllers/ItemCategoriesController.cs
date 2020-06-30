#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
    [Authorize]
    public class ItemCategoriesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly ItemCategoryVMMapper _mapper = new ItemCategoryVMMapper();

        public ItemCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ItemCategories
        public async Task<IActionResult> Index()
        {
            return View((await _bll.ItemCategories.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: ItemCategories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id, User.UserGuidId());

            if (itemCategory == null)
            {
                return NotFound(new MessageDTO($"Item category with id {id} not found"));
            }

            return View(_mapper.Map(itemCategory));
        }

        // GET: ItemCategories/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ItemCategoriesCreateEditViewModel
            {
                CategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), nameof(CategoryBLL.Id), nameof(CategoryBLL.Description)),
                ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description))
            };
            
            return View(vm);
        }

        // POST: ItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCategoriesCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.ItemCategories.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }

            vm.CategorySelectList =
                new SelectList(await _bll.Categories.GetAllAsync(), nameof(CategoryBLL.Id), nameof(CategoryBLL.Description), vm.CategoryId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // GET: ItemCategories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemCategory == null)
            {
                return NotFound(new MessageDTO("ItemCategory not found"));
            }

            var vm = _mapper.Map(itemCategory);
            
            vm.CategorySelectList =
                new SelectList(await _bll.Categories.GetAllAsync(), nameof(CategoryBLL.Id), nameof(CategoryBLL.Description), vm.CategoryId);
            
            
            return View(vm);
        }

        // POST: ItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemCategoriesCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.ItemCategories.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Item category with this id {id} not found"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.ItemCategories.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.CategorySelectList =
                new SelectList(await _bll.Categories.GetAllAsync(), nameof(CategoryBLL.Id), nameof(CategoryBLL.Description), vm.CategoryId);
            
            
            return View(vm);
        }

        // GET: ItemCategories/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id, User.UserGuidId());
            
            if (itemCategory == null)
            {
                return NotFound(new MessageDTO("ItemCategory not found"));
            }

            return View(_mapper.Map(itemCategory));
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id, User.UserGuidId());
            var categoriesCount = _bll.ItemCategories.GetItemCategoriesCount(User.UserGuidId(), itemCategory.ItemId);
            var vm = _mapper.Map(itemCategory);
            if (categoriesCount <= 1)
            {
                Console.WriteLine("Tuleb siia!");
                ModelState.AddModelError("categoryCount","Can't delete. Item must have 1 category");
                return View(vm);
            }
            await _bll.ItemCategories.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
