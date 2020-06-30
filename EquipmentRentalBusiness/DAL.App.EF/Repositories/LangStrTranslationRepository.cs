using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class LangStrTranslationRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.LangStrTranslation,
            DAL.App.DTO.LangStrTranslation>,
        ILangStrTranslationRepository
    {
        public LangStrTranslationRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.LangStrTranslation, DTO.LangStrTranslation>())
        {
        }

        public override async Task<IEnumerable<LangStrTranslation>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.LangStr)
                .OrderBy(l => l.LangStrId);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            foreach (var translation in result)
            {
                Console.WriteLine(translation.Value);
            }
            return result;
        }

        public override async Task<LangStrTranslation> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.LangStr);
            var domainItem = await query.FirstOrDefaultAsync(i => i.Id == id);
            var result = Mapper.Map(domainItem);
            return result;
        }
    }
}