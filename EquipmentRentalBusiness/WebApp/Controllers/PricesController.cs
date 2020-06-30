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
    [Authorize]
    public class PricesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PriceVMMapper _mapper = new PriceVMMapper();

        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Prices
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Prices.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (price == null)
            {
                return NotFound(new MessageDTO($"Price with id {id} not found"));
            }

            return View(_mapper.Map(price));
        }

        // GET: Prices/Create
        public ViewResult Create()
        {
            var vm = new PriceCreateEditViewModel
            {
                
            };
            
            return View(vm);
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PriceCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.Prices.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id, User.UserGuidId());
            if (price == null)
            {
                return NotFound(new MessageDTO("Price not found"));
            }

            var vm = _mapper.Map(price);

            return View(vm);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PriceCreateEditViewModel vm)
        {
            Console.WriteLine(vm.ItemId);
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.Prices.ExistsAsync(id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Price with this id {id} not found"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Prices.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id, User.UserGuidId());
            if (price == null)
            {
                return NotFound(new MessageDTO("Price not found"));
            }

            return View(_mapper.Map(price));
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Prices.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
