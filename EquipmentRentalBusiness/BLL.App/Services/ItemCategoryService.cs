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
using PublicApi.DTO.v1.ItemCategoryDTOs;

namespace BLL.App.Services
{
    public class ItemCategoryService : BaseEntityService<IAppUnitOfWork, IItemCategoryRepository, IItemCategoryServiceMapper, ItemCategoryDAL, ItemCategoryBLL>, IItemCategoryService
    {
        public ItemCategoryService(IAppUnitOfWork uow) 
            : base(uow, uow.ItemCategories, new ItemCategoryServiceMapper())
        {
        }

        public int GetItemCategoriesCount(Guid userId, Guid itemId)
        {
            var itemCategories = UOW.ItemCategories.GetAllAsync(userId).Result;
            var count = 0;
            foreach (var itemCategory in itemCategories)
            {
                if (itemCategory.ItemId == itemId)
                {
                    count++;
                }
            }

            return count;
        }
    }
}