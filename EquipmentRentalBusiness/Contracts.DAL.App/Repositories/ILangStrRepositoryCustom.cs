using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILangStrRepositoryCustom: ILangStrRepositoryCustom<LangStr>
    {
        
    }
    public interface ILangStrRepositoryCustom<TLangStr>
    {
        Task<IEnumerable<TLangStr>> GetLanguageStrings();
    }
}