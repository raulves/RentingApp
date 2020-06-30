
using DAL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppUserCompanyRepository : IBaseRepository<AppUserCompanyDAL>, IAppUserCompanyRepositoryCustom
    {
    }
    
}