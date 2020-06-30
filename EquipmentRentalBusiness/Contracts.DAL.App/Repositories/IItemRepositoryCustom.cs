using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IItemRepositoryCustom : IItemRepositoryCustom<ItemDAL, ItemView, SingleItemView>
    {
    }

    public interface IItemRepositoryCustom<TItemDAL, TItemView, TSingleItemView>
    {
        Task<IEnumerable<TItemView>> GetItemsForViewAsync(Guid? categoryId, string? search);
        Task<TSingleItemView> GetItemViewAsync(Guid id);
        
        Task<IEnumerable<TItemDAL>> GetAppUserCompaniesItems(Guid userId, bool noTracking = true);
        Task<IEnumerable<TItemDAL>> GetAppUserItems(Guid userId, bool noTracking = true);
        
    }
    
    
    
}