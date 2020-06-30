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
using PublicApi.DTO.v1.PriceDTOs;

namespace BLL.App.Services
{
    public class PriceService : BaseEntityService<IAppUnitOfWork, IPriceRepository, IPriceServiceMapper, PriceDAL, PriceBLL>, IPriceService
    {
        public PriceService(IAppUnitOfWork uow) 
            : base(uow, uow.Prices, new PriceServiceMapper())
        {
        }

    }
}