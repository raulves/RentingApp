using System;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface ICompanyService : IBaseEntityService<CompanyBLL>, ICompanyRepositoryCustom<CompanyBLL>
    {
        // TODO : add custom methods
    }
}