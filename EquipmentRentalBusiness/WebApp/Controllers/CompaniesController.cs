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
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly CompanyVMMapper _mapper = new CompanyVMMapper();

        public CompaniesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: AppUser Companies
        public async Task<IActionResult> AppUserCompanies()
        {
            return View((await _bll.Companies.GetAllAppUserCompaniesAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Companies.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var company = await _bll.Companies.FirstOrDefaultAsync(id);

            if (company == null)
            {
                return NotFound(new MessageDTO($"Company with id {id} not found"));
            }

            return View(_mapper.Map(company));
        }

        // GET: Companies/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CompanyCreateEditViewModel
            {
                LocationSelectList = new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine))
            };
            
            return View(vm);
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.Companies.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                var appUserCompanyBLL = new AppUserCompanyBLL()
                {
                    AppUserId = User.UserGuidId(),
                    CompanyId = bllEntity.Id
                };
                _bll.AppUserCompanies.Add(appUserCompanyBLL);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(AppUserCompanies));
            }
            vm.LocationSelectList = new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine), vm.LocationId);
            
            return View(vm);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            
            var company = await _bll.Companies.FirstOrDefaultAsync(id, User.UserGuidId());

            if (company == null)
            {
                return NotFound(new MessageDTO($"Company with id {id} not found"));
            }
            
            var vm = _mapper.Map(company);
            
            vm.LocationSelectList = new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine), vm.LocationId);
            
            return View(vm);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CompanyCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.Companies.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have company with this id {id}"));
            }
            
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Companies.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(AppUserCompanies));
            }
            vm.LocationSelectList = new SelectList(await _bll.Locations.GetAllAsync(User.UserGuidId()), nameof(LocationBLL.Id), nameof(LocationBLL.AddressLine), vm.LocationId);
            
            return View(vm);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var company = await _bll.Companies.FirstOrDefaultAsync(id, User.UserGuidId());

            if (company == null)
            {
                return NotFound(new MessageDTO($"Company with id {id} not found"));
            }

            return View(_mapper.Map(company));
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Companies.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(AppUserCompanies));
        }
        
    }
}
