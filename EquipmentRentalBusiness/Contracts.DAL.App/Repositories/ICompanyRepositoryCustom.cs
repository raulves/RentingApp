using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICompanyRepositoryCustom : ICompanyRepositoryCustom<CompanyDAL>
    {
    }

    public interface ICompanyRepositoryCustom<TCompanyDAL>
    {
        Task<IEnumerable<TCompanyDAL>> GetAllAppUserCompaniesAsync(Guid userId, bool noTracking = true);
    }
}