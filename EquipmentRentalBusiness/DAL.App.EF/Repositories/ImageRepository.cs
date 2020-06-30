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
    public class ImageRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Image, ImageDAL>, IImageRepository
    {
        public ImageRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Image, ImageDAL>())
        {
        }

        public override async Task<IEnumerable<ImageDAL>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Item);
                
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<ImageDAL> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Item);
                
            var domainItem = await query.FirstOrDefaultAsync(i =>i.Id == id);
            var result = Mapper.Map(domainItem);
            return result;
        }
    }
}