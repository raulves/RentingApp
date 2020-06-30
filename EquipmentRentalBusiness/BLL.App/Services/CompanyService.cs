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
using PublicApi.DTO.v1.CompanyDTOs;

namespace BLL.App.Services
{
    public class CompanyService : BaseEntityService<IAppUnitOfWork, ICompanyRepository, ICompanyServiceMapper, CompanyDAL, CompanyBLL>, ICompanyService
    {
        public CompanyService(IAppUnitOfWork uow) 
            : base(uow, uow.Companies, new CompanyServiceMapper())
        {
        }

        public async Task<IEnumerable<CompanyBLL>> GetAllAppUserCompaniesAsync(Guid userId, bool noTracking = true)
        {
            return (await Repository.GetAllAppUserCompaniesAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}