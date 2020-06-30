#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;

using Extensions;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using WebApp.ViewModels;
using WebApp.ViewModels.Mappers;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class RentalPeriodsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly RentalPeriodVMMapper _mapper = new RentalPeriodVMMapper();

        public RentalPeriodsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: RentalPeriods
        public async Task<IActionResult> Index()
        {
            return View((await _bll.RentalPeriods.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: RentalPeriods/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var rentalPeriod = await _bll.RentalPeriods.FirstOrDefaultAsync(id);

            if (rentalPeriod == null)
            {
                return NotFound(new MessageDTO($"Rental period with id {id} not found"));
            }

            return View(_mapper.Map(rentalPeriod));
        }

        // GET: RentalPeriods/Create
        public IActionResult Create()
        {
            var vm = new RentalPeriodCreateEditViewModel();
            return View(vm);
        }

        // POST: RentalPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalPeriodCreateEditViewModel vm)
        {

            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.RentalPeriods.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: RentalPeriods/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var rentalPeriod = await _bll.RentalPeriods.FirstOrDefaultAsync(id);
            if (rentalPeriod == null)
            {
                return NotFound(new MessageDTO("RentalPeriod not found"));
            }

            var vm = _mapper.Map(rentalPeriod);
            
            return View(vm);
        }

        // POST: RentalPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RentalPeriodCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and rentalPeriodEditDTO.id do not match"));
            }
            
            if (!await _bll.RentalPeriods.ExistsAsync(vm.Id))
            {
                return NotFound(new MessageDTO($"Rental period with this id {id} not found"));
            }

            if (ModelState.IsValid)
            {
                await _bll.RentalPeriods.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: RentalPeriods/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var rentalPeriod = await _bll.RentalPeriods.FirstOrDefaultAsync(id);
            if (rentalPeriod == null)
            {
                return NotFound(new MessageDTO("RentalPeriod not found"));
            }

            return View(_mapper.Map(rentalPeriod));
        }

        // POST: RentalPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.RentalPeriods.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
