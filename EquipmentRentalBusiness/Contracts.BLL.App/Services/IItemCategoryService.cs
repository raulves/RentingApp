using System;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IItemCategoryService : IBaseEntityService<ItemCategoryBLL>, IItemCategoryRepositoryCustom<ItemCategoryBLL>
    {
        // TODO : add custom methods
        int GetItemCategoriesCount(Guid userId, Guid itemId);
    }
}