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
    public class PaymentTypesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PaymentTypeVMMapper _mapper = new PaymentTypeVMMapper();

        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            return View((await _bll.PaymentTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound(new MessageDTO($"Payment type with id {id} not found"));
            }

            return View(_mapper.Map(paymentType));
        }

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            var vm = new PaymentTypeCreateEditViewModel();
            return View(vm);
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.PaymentTypes.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound(new MessageDTO("PaymentType not found"));
            }

            var vm = _mapper.Map(paymentType);
            
            return View(vm);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentTypeCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.PaymentTypes.ExistsAsync(vm.Id))
            {
                return NotFound(new MessageDTO($"Payment type with this id {id} not found"));
            }

            if (ModelState.IsValid)
            {
                await _bll.PaymentTypes.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound(new MessageDTO("PaymentType not found"));
            }

            return View(_mapper.Map(paymentType));
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.PaymentTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
