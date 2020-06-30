using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepositoryCustom : ICategoryRepositoryCustom<CategoryDAL>
    {
    }

    public interface ICategoryRepositoryCustom<TCategoryDAL>
    {
        Task<IEnumerable<TCategoryDAL>> GetCategoriesForSelectListAsync(Guid? userId = null, bool noTracking = true);
    }
}