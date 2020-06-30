using System;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IItemDescriptionService : IBaseEntityService<ItemDescriptionBLL>, IItemDescriptionRepositoryCustom<ItemDescriptionBLL>
    {
        // TODO : add custom methods
    }
}