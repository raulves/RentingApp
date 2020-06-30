using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;

using Domain.App;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LangStrRepository :
        EFBaseRepository<AppDbContext, AppUser, LangStr, DAL.App.DTO.LangStr>,
        ILangStrRepository
    {
        public LangStrRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<LangStr, DAL.App.DTO.LangStr>())
        {
        }

        public async Task<IEnumerable<DTO.LangStr>> GetLanguageStrings()
        {
            return await RepoDbSet.AsNoTracking()
                .Include(l => l.Translations)
                .Select(a => new DTO.LangStr()
                {
                    Id = a.Id,
                    CultureCount = a.Translations!.Count,
                    CurrentValue = a
                })
                .ToListAsync();
        }
    }
}