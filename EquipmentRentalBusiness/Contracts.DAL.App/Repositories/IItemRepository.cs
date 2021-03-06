using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using DAL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.DAL.Base.Repositories;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.ItemDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IItemRepository : IBaseRepository<ItemDAL>, IItemRepositoryCustom
    {
        
    }
    
    
}