#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using BLL.App.Mappers;
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
    public class InvoicesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceVMMapper _mapper = new InvoiceVMMapper();

        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Invoices.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound(new MessageDTO($"AppUser invoice with id {id} not found"));
            }

            return View(_mapper.Map(invoice));
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            var bookings = (await _bll.Bookings.GetAllAsync(User.UserGuidId())).ToList();

            if (bookings.Count == 0)
            {
                return RedirectToAction("Index", "Bookings");
            }

            var vm = new InvoiceCreateEditViewModel
            {
                InvoiceWithoutVat = _bll.Invoices.CalculateInvoiceTotalWithoutVAT(bookings),
                Vat = _bll.Invoices.CalculateInvoiceVAT(bookings),
                InvoiceTotal = _bll.Invoices.CalculateInvoiceTotalWithVAT(bookings),
                Bookings = (bookings).Select(e => new VMMapper<BookingBLL, BookingsCreateEditViewModel>().Map(e)),
                CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()),
                    nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName)),
                PaymentTypeSelectList = new SelectList(await _bll.PaymentTypes.GetAllAsync(), nameof(PaymentTypeBLL.Id),
                    nameof(PaymentTypeBLL.Description))
            };


            return View(vm);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceCreateEditViewModel vm)
        {
            // Pärin kõik bookingud
            var bookings = (await _bll.Bookings.GetAllAsync(User.UserGuidId()));
            vm.AppUserId = User.UserGuidId();
            vm.InvoiceNumber = _bll.Invoices.GetInvoiceNumber();
            vm.InvoiceDate = DateTime.Now;
            vm.VatPercent = 20;
            
            var invoiceBLL = _mapper.Map(vm);
            _bll.Invoices.Add(invoiceBLL);
            await _bll.SaveChangesAsync();
            vm.Id = invoiceBLL.Id;
            
            // Add invoice id to bookings
            // ID saan alles pärast SaveChanges
            foreach (var booking in bookings)
            {
                var bookingBLL = _bll.Bookings.FirstOrDefaultAsync(booking.Id, User.UserGuidId()).Result;
                bookingBLL.InvoiceId = invoiceBLL.Id;
                
                await _bll.Bookings.UpdateAsync(bookingBLL);
            }
            await _bll.SaveChangesAsync();


            
            // Make payment
            var paymentBLL = new PaymentBLL()
            {
                AppUserId = User.UserGuidId(),
                Amount = vm.InvoiceTotal,
                PaymentDate = DateTime.Now,
                PaymentTypeId = vm.PaymentTypeId,
                InvoiceId = invoiceBLL.Id
            };
            _bll.Payments.Add(paymentBLL);
            await _bll.SaveChangesAsync();
            

            return RedirectToAction("Index", "Items");
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound(new MessageDTO($"AppUser invoice with id {id} not found"));
            }

            var vm = _mapper.Map(invoice);

            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()),
                nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);

            return View(vm);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InvoiceCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }

            if (!await _bll.Invoices.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have invoice with this id {id}"));
            }

            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Invoices.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.CompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()),
                nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);

            return View(vm);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound(new MessageDTO($"AppUser invoice with id {id} not found"));
            }

            return View(_mapper.Map(invoice));
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Invoices.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}