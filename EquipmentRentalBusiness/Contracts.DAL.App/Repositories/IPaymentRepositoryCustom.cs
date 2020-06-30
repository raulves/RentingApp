using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepositoryCustom : IPaymentRepositoryCustom<PaymentDAL>
    {
    }

    public interface IPaymentRepositoryCustom<TPaymentDAL>
    {
        
    }
}