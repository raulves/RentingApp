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
    public class BookingsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly BookingVMMapper _mapper = new BookingVMMapper();

        public BookingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View((await _bll.Bookings.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }
        
        // GET: AppUser Bookings as a renter
        // Paid bookings
        public async Task<IActionResult> AppUserBookingsAsRenter()
        {
            return View((await _bll.Bookings.GetAppUserBookingsAsRenter(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }
        
        // GET: AppUser Bookings as a owner
        // Paid bookings
        public async Task<IActionResult> AppUserBookingsAsOwner()
        {
            return View((await _bll.Bookings.GetAppUserBookingsAsOwner(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }
        
        // GET: AppUser companies Bookings as a owner
        // Paid bookings
        public async Task<IActionResult> AppUserCompaniesBookingsAsOwner()
        {
            return View((await _bll.Bookings.GetAppUserCompaniesBookingsAsOwner(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }
        
        // GET: AppUser companies Bookings as a owner
        // Paid bookings
        public async Task<IActionResult> AppUserCompaniesBookingsAsRenter()
        {
            return View((await _bll.Bookings.GetAppUserCompaniesBookingsAsRenter(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var booking = await _bll.Bookings.FirstOrDefaultAsync(id, User.UserGuidId());

            if (booking == null)
            {
                return NotFound(new MessageDTO($"Booking with id {id} not found"));
            }

            return View(_mapper.Map(booking));
        }

        // GET: Bookings/Create
        [AllowAnonymous]
        public async Task<IActionResult> Create(Guid id)
        {
            var item = await _bll.Items.GetItemViewAsync(id);

            if (item == null)
            {
                return NotFound(new MessageDTO($"Item with id {id} not found"));
            }

            if (item.Images!.Count == 0)
            {
                item.Images = new List<ImageBLL>();
            }
            var itemVM = new VMMapper<SingleItemView, SingleItemViewVM>().Map(item);
            
            if (User.Identity.IsAuthenticated)
            {
                itemVM.CompanySelectList = new SelectList(await _bll.Companies.GetAllAppUserCompaniesAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName));
            }
            
            
            return View(itemVM);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, SingleItemViewVM vm)
        {
            var item = await _bll.Items.GetItemViewAsync(id);
            item.BookingStartDay = vm.BookingStartDay;
            item.BookingEndDay = vm.BookingEndDay;
            item.CompanyId = vm.CompanyId;

            if (vm.BookingStartDay < DateTime.Now)
            {
                ModelState.AddModelError(nameof(vm.BookingStartDay), "Booking start date have to be today or future date.");
                var itemVM = new VMMapper<SingleItemView, SingleItemViewVM>().Map(item);
                itemVM.CompanySelectList = new SelectList(await _bll.Companies.GetAllAppUserCompaniesAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
                return View(itemVM);
            }
            
            if (vm.BookingEndDay.Subtract(vm.BookingStartDay).Days <= 0)
            {
                ModelState.AddModelError(nameof(vm.BookingStartDay), "Booking period cannot be 0 or less.");
                var itemVM = new VMMapper<SingleItemView, SingleItemViewVM>().Map(item);
                itemVM.CompanySelectList = new SelectList(await _bll.Companies.GetAllAppUserCompaniesAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
                return View(itemVM);
            }
            
            if (item.AppUserId == User.UserGuidId())
            {
                ModelState.AddModelError(nameof(vm.BookingStartDay), "Item owner can't make a booking.");
                var itemVM = new VMMapper<SingleItemView, SingleItemViewVM>().Map(item);
                itemVM.CompanySelectList = new SelectList(await _bll.Companies.GetAllAppUserCompaniesAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.CompanyId);
                return View(itemVM);
            }
            
            
            var bookingBLL = _bll.Bookings.CreateBooking(User.UserGuidId(), item);
            _bll.Bookings.Add(bookingBLL);
            await _bll.SaveChangesAsync();
            
            // Add ItemBooked
            var itemBookedBLL = new ItemBookedBLL();
            itemBookedBLL.AppUserId = User.UserGuidId();
            itemBookedBLL.DateFrom = vm.BookingStartDay;
            itemBookedBLL.DateTo = vm.BookingEndDay;
            itemBookedBLL.ItemId = vm.Id;
            itemBookedBLL.BookingId = bookingBLL.Id;
            _bll.ItemsBooked.Add(itemBookedBLL);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Create), new {id = id});
        }

        /*// GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var booking = await _bll.Bookings.FirstOrDefaultAsync(id, User.UserGuidId());

            if (booking == null)
            {
                return NotFound(new MessageDTO($"Booking with id {id} not found"));
            }

            var vm = _mapper.Map(booking);
            
            vm.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(InvoiceBLL.Id), nameof(InvoiceBLL.InvoiceNumber), vm.InvoiceId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            //vm.ItemOwnerSelectList = new SelectList(_context.Users, nameof(AppUser.Id), nameof(AppUser.FullName), vm.Booking.ItemOwnerId);
            vm.ItemOwnerCompanySelectList =  new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.ItemOwnerCompanyId);
            //vm.RentalPeriodSelectList = new SelectList(await _bll.RentalPeriods.GetAllAsync(), nameof(RentalPeriodBLL.Id), nameof(RentalPeriodBLL.Description), vm.RentalPeriodId);
            //vm.RenterSelectList = new SelectList(_context.Users, nameof(AppUser.Id), nameof(AppUser.FullName), vm.Booking.RenterId);
            vm.RenterCompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.RenterCompanyId);
            
            return View(vm);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookingsCreateEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest(new MessageDTO("Id and vm.id do not match"));
            }
            
            if (!await _bll.Bookings.ExistsAsync(vm.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have booking with this id {id}"));
            }
            
            vm.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Bookings.UpdateAsync(_mapper.Map(vm));
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(InvoiceBLL.Id), nameof(InvoiceBLL.InvoiceNumber), vm.InvoiceId);
            vm.ItemSelectList = new SelectList(await _bll.Items.GetAllAsync(User.UserGuidId()), nameof(ItemBLL.Id), nameof(ItemBLL.Description), vm.ItemId);
            //vm.ItemOwnerSelectList = new SelectList(_context.Users, nameof(AppUser.Id), nameof(AppUser.FullName), vm.Booking.ItemOwnerId);
            vm.ItemOwnerCompanySelectList =  new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.ItemOwnerCompanyId);
            //vm.RentalPeriodSelectList = new SelectList(await _bll.RentalPeriods.GetAllAsync(), nameof(RentalPeriodBLL.Id), nameof(RentalPeriodBLL.Description), vm.RentalPeriodId);
            //vm.RenterSelectList = new SelectList(_context.Users, nameof(AppUser.Id), nameof(AppUser.FullName), vm.Booking.RenterId);
            vm.RenterCompanySelectList = new SelectList(await _bll.Companies.GetAllAsync(User.UserGuidId()), nameof(CompanyBLL.Id), nameof(CompanyBLL.CompanyName), vm.RenterCompanyId);
            
            return View(vm);
        }*/

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            
            var booking = await _bll.Bookings.FirstOrDefaultAsync(id, User.UserGuidId());

            if (booking == null)
            {
                return NotFound(new MessageDTO($"Booking with id {id} not found"));
            }

            return View(_mapper.Map(booking));
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Bookings.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
