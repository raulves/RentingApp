using System;
using BLL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IProductDescriptionService : IBaseEntityService<ProductDescriptionBLL>, IProductDescriptionRepositoryCustom<ProductDescriptionBLL>
    {
        // TODO : add custom methods
    }
}