using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using DAL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.DAL.Base.Repositories;
using PublicApi.DTO.v1.BookingDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IBookingRepository : IBaseRepository<BookingDAL>, IBookingRepositoryCustom
    {
        string? GetLastBookingNumber();
    }
    
}