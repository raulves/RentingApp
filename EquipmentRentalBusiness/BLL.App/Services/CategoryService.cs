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
using PublicApi.DTO.v1.CategoryDTOs;

namespace BLL.App.Services
{
    public class CategoryService : BaseEntityService<IAppUnitOfWork, ICategoryRepository, ICategoryServiceMapper, CategoryDAL, CategoryBLL>, ICategoryService
    {
        public CategoryService(IAppUnitOfWork uow) 
            : base(uow, uow.Categories, new CategoryServiceMapper())
        {
        }

        public async Task<IEnumerable<CategoryBLL>> GetCategoriesForSelectListAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetCategoriesForSelectListAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}