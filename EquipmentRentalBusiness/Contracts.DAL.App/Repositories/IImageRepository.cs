using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using DAL.App.DTO;
using ee.itcollege.Raul.Vesinurm.Contracts.DAL.Base.Repositories;
using PublicApi.DTO.v1.ImageDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IImageRepository : IBaseRepository<ImageDAL>, IImageRepositoryCustom
    {
    }
    
}