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
    public class AppUserCompaniesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly AppUserCompanyVMMapper _mapper = new AppUserCompanyVMMapper();

        public AppUserCompaniesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: AppUserCompany
        public async Task<IActionResult> Index()
        {
            return View((await _bll.AppUserCompanies.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: AppUserCompany/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var appUserCompany = await _bll.AppUserCompanies.FirstOrDefaultAsync(id);
                
            if (appUserCompany == null)
            {
                return NotFound(new MessageDTO($"AppUser company with id {id} not found"));
            }

            return View(_mapper.Map(appUserCompany));
        }

        // GET: AppUserCompany/Create
        public async Task<IActionResult> Create()
        {
            var vm = new AppUserCompanyCreateEditViewModel
            {
                CompaniesSelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName))
            };
            
            return View(vm);
        }

        // POST: AppUserCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUserCompanyCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();
            
            
            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.AppUserCompanies.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            
            vm.CompaniesSelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            
            return View(vm);
        }

        // GET: AppUserCompany/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            
            
            var appUserCompany = await _bll.AppUserCompanies.FirstOrDefaultAsync(id, User.UserGuidId());
            if (appUserCompany == null)
            {
                return NotFound(new MessageDTO("AppUserCompany not found"));
            }

            var vm = _mapper.Map(appUserCompany);

            vm.CompaniesSelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            
            return View(vm);
        }

        // POST: AppUserCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AppUserCompanyCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.AppUserCompanies.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have that company."));
            }
            
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.AppUserCompanies.UpdateAsync(_mapper.Map(vm), User.UserGuidId());
                await _bll.SaveChangesAsync();
                
                
                return RedirectToAction(nameof(Index));
            }
            vm.CompaniesSelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            return View(vm);
        }

        // GET: AppUserCompany/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var appUserCompany = await _bll.AppUserCompanies.FirstOrDefaultAsync(id, User.UserGuidId());
            if (appUserCompany == null)
            {
                return NotFound(new MessageDTO("AppUserCompany not found"));
            }

            return View(_mapper.Map(appUserCompany));
        }

        // POST: AppUserCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.AppUserCompanies.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        
    }
}
