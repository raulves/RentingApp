using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;



using Domain.App;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class PaymentRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Payment, PaymentDAL>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext, 
            new DALMapper<Payment, PaymentDAL>())
        {
        }

        
    }
}