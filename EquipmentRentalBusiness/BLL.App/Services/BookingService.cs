using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.Raul.Vesinurm.BLL.Base.Service;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using SingleItemView = BLL.App.DTO.SingleItemView;

namespace BLL.App.Services
{
    public class BookingService : BaseEntityService<IAppUnitOfWork, IBookingRepository, IBookingServiceMapper, BookingDAL, BookingBLL>, IBookingService
    {

        private const decimal VatPercent = 20;
        
        public BookingService(IAppUnitOfWork uow) 
            : base(uow, uow.Bookings, new BookingServiceMapper())
        {
        }

        public BookingBLL CreateBooking(Guid userId, SingleItemView vm)
        {
            var booking = new BookingBLL()
            {
                AppUserId = userId,
                BookingNumber = GetBookingNumber(),
                BookingDate = DateTime.Now,
                BookingStartDay = vm.BookingStartDay,
                BookingEndDay = vm.BookingEndDay,
                BookingPeriodDays = CalculateBookingPeriodInDays(vm),
                PricePerDay = CalculatePricePerDay(vm),
                VatPercent = 0,
                Vat = 0,
                BookingWithoutVat = CalculateBookingWithoutVat(vm),
                BookingTotal = 0,
                ItemId = vm.Id,
                ItemOwnerId = vm.AppUserId,
                RenterId = userId,
                ItemOwnerCompanyId = vm.ItemOwnerCompanyId,
                RenterCompanyId = vm.CompanyId,
                
            };

            // Arvutan VAT percent-i, kui ItemOwner on Company, siis küsin andmebaasist company ja kui tal KM number, siis 20%. 
            if (booking.ItemOwnerCompanyId != null)
            {
                if (vm.HasVatNumber)
                {
                    booking.VatPercent = VatPercent;
                }
            }
            // Calculate VAT
            booking.Vat = CalculateVat(booking.BookingWithoutVat, booking.VatPercent);
            booking.BookingTotal = booking.Vat + booking.BookingWithoutVat;
            return booking;
        }

        private string GetBookingNumber()
        {
            var bookingNumber = UOW.Bookings.GetLastBookingNumber();
            if (bookingNumber == null)
            {
                return "1";
            }

            var number = 1;
            int.TryParse(bookingNumber, out number);
            var newBookingNumber = number + 1;
            return newBookingNumber.ToString();
        }

        private int CalculateBookingPeriodInDays(SingleItemView vm)
        {
            var bookingPeriod = vm.BookingEndDay.Subtract(vm.BookingStartDay).Days;
            return bookingPeriod;
        }

        private decimal CalculatePricePerDay(SingleItemView vm)
        {
            var bookingPeriod = CalculateBookingPeriodInDays(vm);

            if (bookingPeriod >= 1 && bookingPeriod < 7)
            {
                return vm.PricePerDay;
            }

            if (bookingPeriod >= 7 && bookingPeriod < 30)
            {
                return vm.PricePerWeek / 7;
            }

            return vm.PricePerMonth / 30;
        }

        private decimal CalculateBookingWithoutVat(SingleItemView vm)
        {
            return CalculateBookingPeriodInDays(vm) * CalculatePricePerDay(vm);
        }

        private decimal CalculateVat(decimal total, decimal vatPercent)
        {
            return total * (vatPercent / 100);
        }

        /*private decimal CalculateTotal(SingleItemView vm)
        {
            return CalculateBookingWithoutVat(vm) + CalculateVat();
        }*/

        public async Task<IEnumerable<BookingBLL>> GetAppUserBookingsAsOwner(Guid userId, bool noTracking = true)
        {
            return (await Repository.GetAppUserBookingsAsOwner(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<BookingBLL>> GetAppUserBookingsAsRenter(Guid userId, bool noTracking = true)
        {
            return (await Repository.GetAppUserBookingsAsRenter(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<BookingBLL>> GetAppUserCompaniesBookingsAsOwner(Guid userId, bool noTracking = true)
        {
            return (await Repository.GetAppUserCompaniesBookingsAsOwner(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<BookingBLL>> GetAppUserCompaniesBookingsAsRenter(Guid userId, bool noTracking = true)
        {
            return (await Repository.GetAppUserCompaniesBookingsAsRenter(userId, noTracking))
                .Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<BookingBLL>> GetAllAsyncForAdminVM(Guid? userId, bool noTracking = true)
        {
            return (await Repository.GetAllAsyncForAdminVM(userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}