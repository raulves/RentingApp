using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;



using Domain.App;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class RentalPeriodRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, RentalPeriod, RentalPeriodDAL>, IRentalPeriodRepository
    {
        public RentalPeriodRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<RentalPeriod, RentalPeriodDAL>())
        {
        }
        public Guid GetRentalPeriodId(string period)
        {
            return RepoDbSet.AsNoTracking().Single(d => d.Description.Equals(period)).Id;
        }

        
    }
}