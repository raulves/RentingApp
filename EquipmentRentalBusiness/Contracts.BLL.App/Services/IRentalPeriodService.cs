using System;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IRentalPeriodService : IBaseEntityService<RentalPeriodBLL>, IRentalPeriodRepositoryCustom<RentalPeriodBLL>
    {
        // TODO : add custom methods
        Guid GetRentalPeriodId(string period);
    }
}