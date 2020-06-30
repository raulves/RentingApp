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
    public class PaymentTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, PaymentType, PaymentTypeDAL>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<PaymentType, PaymentTypeDAL>())
        {
        }

        public override async Task<IEnumerable<PaymentTypeDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations)
                .OrderBy(a => a.Description);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<PaymentTypeDAL> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations)
                .OrderBy(a => a.Description);
            var domainItem = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainItem);
            return result;
        }
    }
}