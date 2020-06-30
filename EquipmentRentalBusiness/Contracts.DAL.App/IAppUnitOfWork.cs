using Contracts.DAL.App.Repositories;
using ee.itcollege.Raul.Vesinurm.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        
        ILangStrRepository LangStrs { get; }
        ILangStrTranslationRepository LangStrTranslations { get; }
        IAppUserCompanyRepository AppUserCompanies { get; }
        IBookingRepository Bookings { get; }
        ICategoryRepository Categories { get; }
        ICompanyRepository Companies { get; }
        IImageRepository Images { get; }
        IInvoiceRepository Invoices { get; }
        IItemBookedRepository ItemsBooked { get; }
        IItemCategoryRepository ItemCategories { get; }
        IItemDescriptionRepository ItemDescriptions { get; }
        IItemOwnershipRepository ItemOwnerships { get; }
        IItemRepository Items { get; }
        ILocationRepository Locations { get; }
        IPaymentRepository Payments { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        IPriceRepository Prices { get; }
        IProductDescriptionRepository ProductDescriptions { get; }
        IRentalPeriodRepository RentalPeriods { get; }
    }
}