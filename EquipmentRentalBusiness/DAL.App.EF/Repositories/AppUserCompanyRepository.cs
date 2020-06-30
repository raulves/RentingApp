using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;


using Domain.App;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class AppUserCompanyRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.AppUserCompany, AppUserCompanyDAL>, IAppUserCompanyRepository
    {
        public AppUserCompanyRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<AppUserCompany, AppUserCompanyDAL>())
        {
        }
        
    }
}