﻿using System;
using BLL.App.Services;
using ee.itcollege.Raul.Vesinurm.BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
 using Contracts.DAL.App;

 namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        public ILangStrService LangStrs =>
            GetService<ILangStrService>(() => new LangStrService(UOW));

        public ILangStrTranslationService LangStrTranslation =>
            GetService<ILangStrTranslationService>(() => new LangStrTranslationService(UOW));

        public IAppUserCompanyService AppUserCompanies => 
            GetService<IAppUserCompanyService>(() => new AppUserCompanyService(UOW));

        public IBookingService Bookings => 
            GetService<IBookingService>(() => new BookingService(UOW));

        public ICategoryService Categories => 
            GetService<ICategoryService>(() => new CategoryService(UOW));

        public ICompanyService Companies => 
            GetService<ICompanyService>(() => new CompanyService(UOW));

        public IImageService Images => 
            GetService<IImageService>(() => new ImageService(UOW));

        public IInvoiceService Invoices => 
            GetService<IInvoiceService>(() => new InvoiceService(UOW));

        public IItemBookedService ItemsBooked => 
            GetService<IItemBookedService>(() => new ItemBookedService(UOW));

        public IItemCategoryService ItemCategories => 
            GetService<IItemCategoryService>(() => new ItemCategoryService(UOW));

        public IItemDescriptionService ItemDescriptions => 
            GetService<IItemDescriptionService>(() => new ItemDescriptionService(UOW));

        public IItemOwnershipService ItemOwnerships => 
            GetService<IItemOwnershipService>(() => new ItemOwnershipService(UOW));

        public IItemService Items => 
            GetService<IItemService>(() => new ItemService(UOW));

        public ILocationService Locations => 
            GetService<ILocationService>(() => new LocationService(UOW));

        public IPaymentService Payments => 
            GetService<IPaymentService>(() => new PaymentService(UOW));

        public IPaymentTypeService PaymentTypes => 
            GetService<IPaymentTypeService>(() => new PaymentTypeService(UOW));

        public IPriceService Prices => 
            GetService<IPriceService>(() => new PriceService(UOW));

        public IProductDescriptionService ProductDescriptions => 
            GetService<IProductDescriptionService>(() => new ProductDescriptionService(UOW));

        public IRentalPeriodService RentalPeriods => 
            GetService<IRentalPeriodService>(() => new RentalPeriodService(UOW));
        
        public ILangStrService LanguageStrings => 
            GetService<ILangStrService>(() => new LangStrService(UOW));
        
        public ILangStrTranslationService LanguageStringTranslations => 
            GetService<ILangStrTranslationService>(() => new LangStrTranslationService(UOW));
    }
}