using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IBookingService : IBaseEntityService<BookingBLL>, IBookingRepositoryCustom<BookingBLL>
    {
        // TODO : add custom methods
        BookingBLL CreateBooking(Guid userId, SingleItemView vm);
        
    }
}