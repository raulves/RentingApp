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

namespace BLL.App.Services
{
    public class LangStrService :
        BaseEntityService<IAppUnitOfWork, ILangStrRepository, ILangStrServiceMapper,
            DAL.App.DTO.LangStr, BLL.App.DTO.LangStr>, ILangStrService
    {
        public LangStrService(IAppUnitOfWork uow) : base(uow, uow.LangStrs, new LangStrServiceMapper())
        {
        }

        public async Task<IEnumerable<LangStr>> GetLanguageStrings()
        {
            return (await Repository.GetLanguageStrings()).Select(e => Mapper.Map(e));
        }
    }
}