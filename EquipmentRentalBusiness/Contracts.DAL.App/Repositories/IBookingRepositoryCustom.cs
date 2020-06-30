using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IBookingRepositoryCustom : IBookingRepositoryCustom<BookingDAL>
    {
    }

    public interface IBookingRepositoryCustom<TBookingDAL>
    {
        Task<IEnumerable<TBookingDAL>> GetAppUserBookingsAsOwner(Guid userId, bool noTracking = true);
        Task<IEnumerable<TBookingDAL>> GetAppUserBookingsAsRenter(Guid userId, bool noTracking = true);
        Task<IEnumerable<TBookingDAL>> GetAppUserCompaniesBookingsAsOwner(Guid userId, bool noTracking = true);
        Task<IEnumerable<TBookingDAL>> GetAppUserCompaniesBookingsAsRenter(Guid userId, bool noTracking = true);
        Task<IEnumerable<TBookingDAL>> GetAllAsyncForAdminVM(Guid? userId, bool noTracking = true);
    }
}