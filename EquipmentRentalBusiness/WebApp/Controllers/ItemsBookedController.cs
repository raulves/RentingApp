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
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using WebApp.ViewModels;
using WebApp.ViewModels.Mappers;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class ItemsBookedController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly ItemBookedVMMapper _mapper = new ItemBookedVMMapper();

        public ItemsBookedController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ItemsBooked
        public async Task<IActionResult> Index()
        {
            return View((await _bll.ItemsBooked.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: ItemsBooked/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var itemBooked = await _bll.ItemsBooked.FirstOrDefaultAsync(id);

            if (itemBooked == null)
            {
                return NotFound(new MessageDTO($"Booked Item with id {id} not found"));
            }

            return View(_mapper.Map(itemBooked));
        }

        // GET: ItemsBooked/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ItemsBookedCreateEditViewModel
            {
                BookingSelectList = new SelectList(await _bll.Bookings.GetAllAsync(User.UserGuidId()), nameof(BookingBLL.Id), nameof(BookingBLL.BookingNumber)),
                ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description))
            };
            
            return View(vm);
        }

        // POST: ItemsBooked/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemsBookedCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.ItemsBooked.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }

            vm.BookingSelectList = new SelectList(await _bll.Bookings.GetAllAsync(User.UserGuidId()), nameof(BookingBLL.Id), nameof(BookingBLL.BookingNumber), vm.BookingId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // GET: ItemsBooked/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var itemBooked = await _bll.ItemsBooked.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemBooked == null)
            {
                return NotFound(new MessageDTO("ItemBooked not found"));
            }

            var vm = _mapper.Map(itemBooked);
            
            vm.BookingSelectList = new SelectList(await _bll.Bookings.GetAllAsync(User.UserGuidId()), nameof(BookingBLL.Id), nameof(BookingBLL.BookingNumber), vm.BookingId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // POST: ItemsBooked/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemsBookedCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.ItemsBooked.ExistsAsync(id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Booked Item with this id {id} not found"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.ItemsBooked.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.BookingSelectList = new SelectList(await _bll.Bookings.GetAllAsync(User.UserGuidId()), nameof(BookingBLL.Id), nameof(BookingBLL.BookingNumber), vm.BookingId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            
            return View(vm);
        }

        // GET: ItemsBooked/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var itemBooked = await _bll.ItemsBooked.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemBooked == null)
            {
                return NotFound(new MessageDTO("ItemBooked not found"));
            }

            return View(_mapper.Map(itemBooked));
        }

        // POST: ItemsBooked/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ItemsBooked.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
