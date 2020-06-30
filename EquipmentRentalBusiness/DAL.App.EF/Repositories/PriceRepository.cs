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
    public class PriceRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Price, PriceDAL>, IPriceRepository
    {
        public PriceRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Price, PriceDAL>())
        {
        }

        public override async Task<IEnumerable<PriceDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Item)
                .Include(r => r.RentalPeriod);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<PriceDAL> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Item)
                .Include(r => r.RentalPeriod);
            var domainItem = await query.FirstOrDefaultAsync(i => i.Id == id);
            var result = Mapper.Map(domainItem);
            return result;
        }
    }
}