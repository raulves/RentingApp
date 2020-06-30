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
    public class CategoryRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Category, CategoryDAL>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Category, CategoryDAL>())
        {
        }

        public override async Task<IEnumerable<CategoryDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.ParentCategory)
                .Include(c => c.ChildCategories)
                .Include(i => i.ItemCategories)
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations)
                .OrderBy(a => a.Description);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<CategoryDAL> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.ParentCategory)
                .Include(c => c.ChildCategories)
                .Include(i => i.ItemCategories)
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations);
            var domainItem = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainItem);
            return result;
        }

        public async Task<IEnumerable<CategoryDAL>> GetCategoriesForSelectListAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.ChildCategories)
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations)
                //.Where(a => a.ChildCategories!.Count == 0)
                .OrderBy(a => a.Description);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<CategoryDAL> UpdateAsync(CategoryDAL entity, object? userId = null)
        {
            var domainEntity = Mapper.Map(entity);

            // fix the language string - from mapper we get new ones - so duplicate values will be created in db
            // load back from db the originals
            domainEntity.Description = await RepoDbContext.LangStrs.Include(t => t.Translations).FirstAsync(s => s.Id == domainEntity.DescriptionId);
            domainEntity.Description.SetTranslation(entity.Description);
            
            
            await CheckDomainEntityOwnership(domainEntity, userId);
            
            var trackedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
            var result = Mapper.Map(trackedDomainEntity);
            return result;
        }
    }
}