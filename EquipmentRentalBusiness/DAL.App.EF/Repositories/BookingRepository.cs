using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;

using Domain.App;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BookingRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Booking, BookingDAL>, IBookingRepository
    {
        public BookingRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Booking, BookingDAL>())
        {
        }

        public override async Task<IEnumerable<BookingDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.AppUser)
                .Include(i => i.Item)
                .Include(i => i.ItemOwner)
                .Include(r => r.Renter)
                .Include(i => i.ItemOwnerCompany)
                .Include(r => r.RenterCompany)
                .Include(i => i.Invoice)
                .Where(e => e.Invoice == null);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }
        
        public async Task<IEnumerable<BookingDAL>> GetAllAsyncForAdminVM(Guid? userId, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Item)
                .Where(e => e.Invoice != null);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public string? GetLastBookingNumber()
        {
            var bookings = RepoDbSet.AsNoTracking().OrderByDescending(a => a.BookingNumber).ToListAsync();
            if (bookings.Result.Count == 0)
            {
                return null;
            }
            return bookings.Result.FirstOrDefault().BookingNumber;
            
        }

        public async Task<IEnumerable<BookingDAL>> GetAppUserBookingsAsOwner(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery();
            query = query
                .Include(i => i.Item)
                .Where(e => e.InvoiceId != null && e.ItemOwnerId == userId && e.ItemOwnerCompanyId == null);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<IEnumerable<BookingDAL>> GetAppUserBookingsAsRenter(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery();
            query = query
                .Include(i => i.Item)
                .Where(e => e.InvoiceId != null && e.RenterId == userId && e.RenterCompanyId == null);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<IEnumerable<BookingDAL>> GetAppUserCompaniesBookingsAsOwner(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery();
            query = query
                .Include(i => i.Item)
                .Where(e => e.InvoiceId != null && e.ItemOwnerCompanyId != null);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<IEnumerable<BookingDAL>> GetAppUserCompaniesBookingsAsRenter(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery();
            query = query
                .Include(i => i.Item)
                .Where(e => e.InvoiceId != null && e.RenterCompanyId != null);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        
    }
}