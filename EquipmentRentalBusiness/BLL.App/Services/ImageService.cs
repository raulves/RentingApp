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
using PublicApi.DTO.v1.ImageDTOs;

namespace BLL.App.Services
{
    public class ImageService : BaseEntityService<IAppUnitOfWork, IImageRepository, IImageServiceMapper, ImageDAL, ImageBLL>, IImageService
    {
        public ImageService(IAppUnitOfWork uow) 
            : base(uow, uow.Images, new ImageServiceMapper())
        {
        }
        
    }
}