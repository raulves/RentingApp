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
    public class ItemOwnershipsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly ItemOwnershipVMMapper _mapper = new ItemOwnershipVMMapper();

        public ItemOwnershipsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ItemOwnerships
        public async Task<IActionResult> Index()
        {
            return View((await _bll.ItemOwnerships.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: ItemOwnerships/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var itemOwnership = await _bll.ItemOwnerships.FirstOrDefaultAsync(id, User.UserGuidId());

            if (itemOwnership == null)
            {
                return NotFound(new MessageDTO($"Item ownership with id {id} not found"));
            }

            return View(_mapper.Map(itemOwnership));
        }

        // GET: ItemOwnerships/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ItemOwnershipCreateEditViewModel
            {
                CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName)),
                ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description))
            };
            
            return View(vm);
        }

        // POST: ItemOwnerships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemOwnershipCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.ItemOwnerships.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            
            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // GET: ItemOwnerships/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var itemOwnership = await _bll.ItemOwnerships.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemOwnership == null)
            {
                return NotFound(new MessageDTO("ItemOwnership not found"));
            }
            
            var vm = _mapper.Map(itemOwnership);
            
            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // POST: ItemOwnerships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemOwnershipCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.ItemOwnerships.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have Item ownership with this id {id}"));
            }

            vm.AppUserId = User.UserGuidId();
            if (vm.CompanyId == Guid.Empty)
            {
                vm.CompanyId = null;
            }

            if (ModelState.IsValid)
            {
                await _bll.ItemOwnerships.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // GET: ItemOwnerships/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var itemOwnership = await _bll.ItemOwnerships.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemOwnership == null)
            {
                return NotFound(new MessageDTO("ItemOwnership not found"));
            }

            return View(_mapper.Map(itemOwnership));
        }

        // POST: ItemOwnerships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ItemOwnerships.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
