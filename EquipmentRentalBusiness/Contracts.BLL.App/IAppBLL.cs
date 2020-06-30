﻿using System;
using Contracts.BLL.App.Services;
using ee.itcollege.Raul.Vesinurm.Contracts.BLL.Base;

 namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IAppUserCompanyService AppUserCompanies { get; }
        public IBookingService Bookings { get; }
        public ICategoryService Categories { get; }
        public ICompanyService Companies { get; }
        public IImageService Images { get; }
        public IInvoiceService Invoices { get; }
        public IItemBookedService ItemsBooked { get; }
        public IItemCategoryService ItemCategories { get; }
        public IItemDescriptionService ItemDescriptions { get; }
        public IItemOwnershipService ItemOwnerships { get; }
        public IItemService Items { get; }
        public ILocationService Locations { get; }
        public IPaymentService Payments { get; }
        public IPaymentTypeService PaymentTypes { get; }
        public IPriceService Prices { get; }
        public IProductDescriptionService ProductDescriptions { get; }
        public IRentalPeriodService RentalPeriods { get; }
        public ILangStrService LanguageStrings { get; }
        public ILangStrTranslationService LanguageStringTranslations { get; }
    }
}