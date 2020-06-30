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
using PublicApi.DTO.v1.ItemDTOs;
using ItemView = BLL.App.DTO.ItemView;
using SingleItemView = BLL.App.DTO.SingleItemView;

namespace BLL.App.Services
{
    public class ItemService : BaseEntityService<IAppUnitOfWork, IItemRepository, IItemServiceMapper, ItemDAL, ItemBLL>, IItemService
    {
        public ItemService(IAppUnitOfWork uow) 
            : base(uow, uow.Items, new ItemServiceMapper())
        {
        }

        public virtual  async Task<IEnumerable<ItemView>> GetItemsForViewAsync(Guid? categoryId, string? search)
        {
            return (await Repository.GetItemsForViewAsync(categoryId, search)).Select(e => Mapper.MapItemView(e));
        }

        public virtual async Task<SingleItemView> GetItemViewAsync(Guid id)
        {
            return Mapper.MapSingleItemView(await Repository.GetItemViewAsync(id));
        }

        public async Task<IEnumerable<ItemBLL>> GetAppUserCompaniesItems(Guid userId, bool noTracking = true)
        {
            return (await Repository.GetAppUserCompaniesItems(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<ItemBLL>> GetAppUserItems(Guid userId, bool noTracking = true)
        {
            return (await Repository.GetAppUserItems(userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}