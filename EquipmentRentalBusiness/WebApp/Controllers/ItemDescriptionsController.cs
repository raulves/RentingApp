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
    public class ItemDescriptionsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly ItemDescriptionVMMapper _mapper = new ItemDescriptionVMMapper();

        public ItemDescriptionsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ItemDescriptions
        public async Task<IActionResult> Index()
        {
            return View((await _bll.ItemDescriptions.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: ItemDescriptions/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var itemDescription = await _bll.ItemDescriptions.FirstOrDefaultAsync(id);

            if (itemDescription == null)
            {
                return NotFound(new MessageDTO($"ItemDescription with id {id} not found"));
            }

            return View(_mapper.Map(itemDescription));
        }

        // GET: ItemDescriptions/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ItemDescriptionCreateEditViewModel
            {
                ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description)),
                ProductDescriptionSelectList = new SelectList(await _bll.ProductDescriptions.GetAllAsync(), nameof(ProductDescriptionBLL.Id), nameof(ProductDescriptionBLL.Description))
            };
            
            return View(vm);
        }

        // POST: ItemDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemDescriptionCreateEditViewModel vm)
        {

            vm.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.ItemDescriptions.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }

            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            vm.ProductDescriptionSelectList = new SelectList(await _bll.ProductDescriptions.GetAllAsync(),
                nameof(ProductDescriptionBLL.Id), nameof(ProductDescriptionBLL.Description), vm.ProductDescriptionId);
            
            return View(vm);
        }

        // GET: ItemDescriptions/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var itemDescription = await _bll.ItemDescriptions.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemDescription == null)
            {
                return NotFound(new MessageDTO("ItemDescription not found"));
            }
            
            var vm = _mapper.Map(itemDescription);
            
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            vm.ProductDescriptionSelectList = new SelectList(await _bll.ProductDescriptions.GetAllAsync(),
                nameof(ProductDescriptionBLL.Id), nameof(ProductDescriptionBLL.Description), vm.ProductDescriptionId);
            
            return View(vm);
        }

        // POST: ItemDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemDescriptionCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.ItemDescriptions.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"ItemDescription with this id {id} not found"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.ItemDescriptions.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            vm.ProductDescriptionSelectList = new SelectList(await _bll.ProductDescriptions.GetAllAsync(),
                nameof(ProductDescriptionBLL.Id), nameof(ProductDescriptionBLL.Description), vm.ProductDescriptionId);
            
            return View(vm);
        }

        // GET: ItemDescriptions/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var itemDescription = await _bll.ItemDescriptions.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemDescription == null)
            {
                return NotFound(new MessageDTO("ItemDescription not found"));
            }

            return View(_mapper.Map(itemDescription));
        }

        // POST: ItemDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ItemDescriptions.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
