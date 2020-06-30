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
    public class CompanyRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Company, CompanyDAL>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Company, CompanyDAL>())
        {
        }

        
        public async Task<IEnumerable<CompanyDAL>> GetAllAppUserCompaniesAsync(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.Location);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<CompanyDAL> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.Location);
            var domainItem = await query.FirstOrDefaultAsync(i => i.Id == id);
            var result = Mapper.Map(domainItem);
            return result;
        }
    }
}