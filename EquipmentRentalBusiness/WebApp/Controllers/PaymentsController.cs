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
    public class PaymentsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly PaymentVMMapper _mapper = new PaymentVMMapper();

        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Payments.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.UserGuidId());

            if (payment == null)
            {
                return NotFound(new MessageDTO($"AppUser company with id {id} not found"));
            }

            return View(_mapper.Map(payment));
        }

        
        // GET: Payments/Create
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            var vm = new PaymentCreateEditViewModel
            {
                CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName)),
                InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(InvoiceBLL.Id), nameof(InvoiceBLL.InvoiceNumber)),
                PaymentTypeSelectList = new SelectList(await _bll.PaymentTypes.GetAllAsync(), nameof(PaymentTypeBLL.Id), nameof(PaymentTypeBLL.Description))
            };
            
            return View(vm);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(PaymentCreateEditViewModel vm)
        {
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                var bllEntity = _mapper.Map(vm);
                _bll.Payments.Add(bllEntity);
                await _bll.SaveChangesAsync();
                vm.Id = bllEntity.Id;
                return RedirectToAction(nameof(Index));
            }
            
            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            vm.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(InvoiceBLL.Id), nameof(InvoiceBLL.InvoiceNumber), vm.InvoiceId);
            vm.PaymentTypeSelectList = new SelectList(await _bll.PaymentTypes.GetAllAsync(), nameof(PaymentTypeBLL.Id), nameof(PaymentTypeBLL.Description), vm.PaymentTypeId);
            
            return View(vm);
        }

        // GET: Payments/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.UserGuidId());
            if (payment == null)
            {
                return NotFound(new MessageDTO("Payment not found"));
            }

            var vm = _mapper.Map(payment);
            
            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            vm.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(InvoiceBLL.Id), nameof(InvoiceBLL.InvoiceNumber), vm.InvoiceId);
            vm.PaymentTypeSelectList = new SelectList(await _bll.PaymentTypes.GetAllAsync(), nameof(PaymentTypeBLL.Id), nameof(PaymentTypeBLL.Description), vm.PaymentTypeId);
            
            return View(vm);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid id, PaymentCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.Payments.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have payment with this id {id}"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Payments.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
            vm.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(InvoiceBLL.Id), nameof(InvoiceBLL.InvoiceNumber), vm.InvoiceId);
            vm.PaymentTypeSelectList = new SelectList(await _bll.PaymentTypes.GetAllAsync(), nameof(PaymentTypeBLL.Id), nameof(PaymentTypeBLL.Description), vm.PaymentTypeId);
            
            return View(vm);
        }

        // GET: Payments/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.UserGuidId());
            if (payment == null)
            {
                return NotFound(new MessageDTO("Payment not found"));
            }

            return View(_mapper.Map(payment));
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Payments.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
