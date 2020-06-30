using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.Raul.Vesinurm.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public ILangStrRepository LangStrs =>
            GetRepository<ILangStrRepository>(() => new LangStrRepository(UOWDbContext));

        public ILangStrTranslationRepository LangStrTranslations =>
            GetRepository<ILangStrTranslationRepository>(() => new LangStrTranslationRepository(UOWDbContext));

        public IAppUserCompanyRepository AppUserCompanies =>
            GetRepository<IAppUserCompanyRepository>(() => new AppUserCompanyRepository(UOWDbContext));

        public IBookingRepository Bookings =>
            GetRepository<IBookingRepository>(() => new BookingRepository(UOWDbContext));

        public ICategoryRepository Categories =>
            GetRepository<ICategoryRepository>(() => new CategoryRepository(UOWDbContext));

        public ICompanyRepository Companies =>
            GetRepository<ICompanyRepository>(() => new CompanyRepository(UOWDbContext));

        public IImageRepository Images => GetRepository<IImageRepository>(() => new ImageRepository(UOWDbContext));

        public IInvoiceRepository Invoices =>
            GetRepository<IInvoiceRepository>(() => new InvoiceRepository(UOWDbContext));

        public IItemBookedRepository ItemsBooked =>
            GetRepository<IItemBookedRepository>(() => new ItemBookedRepository(UOWDbContext));

        public IItemCategoryRepository ItemCategories =>
            GetRepository<IItemCategoryRepository>(() => new ItemCategoryRepository(UOWDbContext));

        public IItemDescriptionRepository ItemDescriptions =>
            GetRepository<IItemDescriptionRepository>(() => new ItemDescriptionRepository(UOWDbContext));

        public IItemOwnershipRepository ItemOwnerships =>
            GetRepository<IItemOwnershipRepository>(() => new ItemOwnershipRepository(UOWDbContext));

        public IItemRepository Items => GetRepository<IItemRepository>(() => new ItemRepository(UOWDbContext));

        public ILocationRepository Locations =>
            GetRepository<ILocationRepository>(() => new LocationRepository(UOWDbContext));

        public IPaymentRepository Payments =>
            GetRepository<IPaymentRepository>(() => new PaymentRepository(UOWDbContext));

        public IPaymentTypeRepository PaymentTypes =>
            GetRepository<IPaymentTypeRepository>(() => new PaymentTypeRepository(UOWDbContext));

        public IPriceRepository Prices => GetRepository<IPriceRepository>(() => new PriceRepository(UOWDbContext));

        public IProductDescriptionRepository ProductDescriptions =>
            GetRepository<IProductDescriptionRepository>(() => new ProductDescriptionRepository(UOWDbContext));

        public IRentalPeriodRepository RentalPeriods =>
            GetRepository<IRentalPeriodRepository>(() => new RentalPeriodRepository(UOWDbContext));
    }
}