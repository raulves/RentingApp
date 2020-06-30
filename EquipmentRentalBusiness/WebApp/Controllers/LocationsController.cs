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
    [Authorize]
    public class LocationsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly LocationVMMapper _mapper = new LocationVMMapper();

        public LocationsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Locations
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Locations.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var location = await _bll.Locations.FirstOrDefaultAsync(id, User.UserGuidId());

            if (location == null)
            {
                return NotFound(new MessageDTO($"AppUser location with id {id} not found"));
            }

            return View(_mapper.Map(location));
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            
            var vm = new LocationCreateEditViewModel();
            return View(vm);
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.Locations.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var location = await _bll.Locations.FirstOrDefaultAsync(id, User.UserGuidId());
            if (location == null)
            {
                return NotFound(new MessageDTO("Location not found"));
            }

            var vm = _mapper.Map(location);
            
            return View(vm);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, LocationCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.Locations.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have location with this id {id}"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Locations.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var location = await _bll.Locations.FirstOrDefaultAsync(id, User.UserGuidId());
            if (location == null)
            {
                return NotFound(new MessageDTO("Location not found"));
            }

            return View(_mapper.Map(location));
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Locations.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
