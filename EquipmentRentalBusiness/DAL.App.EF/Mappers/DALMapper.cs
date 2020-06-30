using AutoMapper;
using DAL.App.DTO;
using DAL.App.DTO.Identity;

using Domain.App;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.DAL.Base.Mappers;
using PublicApi.DTO.v1.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseDALMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        {
            // add more mappings
            MapperConfigurationExpression.CreateMap<AppRole, AppRoleDAL>();
            MapperConfigurationExpression.CreateMap<AppRoleDAL, AppRole>();
            
            MapperConfigurationExpression.CreateMap<AppUser, AppUserDAL>();
            MapperConfigurationExpression.CreateMap<AppUserDAL, AppUser>();
            
            MapperConfigurationExpression.CreateMap<AppUserCompany, AppUserCompanyDAL>();
            MapperConfigurationExpression.CreateMap<AppUserCompanyDAL, AppUserCompany>();
            
            MapperConfigurationExpression.CreateMap<Booking, BookingDAL>();
            MapperConfigurationExpression.CreateMap<BookingDAL, Booking>();
            
            MapperConfigurationExpression.CreateMap<Category, CategoryDAL>();
            MapperConfigurationExpression.CreateMap<CategoryDAL, Category>();
            
            MapperConfigurationExpression.CreateMap<Company, CompanyDAL>();
            MapperConfigurationExpression.CreateMap<CompanyDAL, Company>();
            
            MapperConfigurationExpression.CreateMap<Image, ImageDAL>();
            MapperConfigurationExpression.CreateMap<ImageDAL, Image>();
            
            MapperConfigurationExpression.CreateMap<Invoice, InvoiceDAL>();
            MapperConfigurationExpression.CreateMap<InvoiceDAL, Invoice>();
            
            MapperConfigurationExpression.CreateMap<Item, ItemDAL>();
            MapperConfigurationExpression.CreateMap<ItemDAL, Item>();
            
            MapperConfigurationExpression.CreateMap<ItemBooked, ItemBookedDAL>();
            MapperConfigurationExpression.CreateMap<ItemBookedDAL, ItemBooked>();
            
            MapperConfigurationExpression.CreateMap<ItemCategory, ItemCategoryDAL>();
            MapperConfigurationExpression.CreateMap<ItemCategoryDAL, ItemCategory>();
            
            MapperConfigurationExpression.CreateMap<ItemDescription, ItemDescriptionDAL>();
            MapperConfigurationExpression.CreateMap<ItemDescriptionDAL, ItemDescription>();
            
            MapperConfigurationExpression.CreateMap<ItemOwnership, ItemOwnershipDAL>();
            MapperConfigurationExpression.CreateMap<ItemOwnershipDAL, ItemOwnership>();
            
            MapperConfigurationExpression.CreateMap<Location, LocationDAL>();
            MapperConfigurationExpression.CreateMap<LocationDAL, Location>();
            
            MapperConfigurationExpression.CreateMap<Payment, PaymentDAL>();
            MapperConfigurationExpression.CreateMap<PaymentDAL, Payment>();
            
            MapperConfigurationExpression.CreateMap<PaymentType, PaymentTypeDAL>();
            MapperConfigurationExpression.CreateMap<PaymentTypeDAL, PaymentType>();

            MapperConfigurationExpression.CreateMap<Price, PriceDAL>();
            MapperConfigurationExpression.CreateMap<PriceDAL, Price>();
            
            MapperConfigurationExpression.CreateMap<ProductDescription, ProductDescriptionDAL>();
            MapperConfigurationExpression.CreateMap<ProductDescriptionDAL, ProductDescription>();
            
            MapperConfigurationExpression.CreateMap<RentalPeriod, RentalPeriodDAL>();
            MapperConfigurationExpression.CreateMap<RentalPeriodDAL, RentalPeriod>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.LangStrTranslation, DAL.App.DTO.LangStrTranslation>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.LangStrTranslation, Domain.App.LangStrTranslation>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.LangStr, LangStrIndexDAL>();
            MapperConfigurationExpression.CreateMap<LangStrIndexDAL, Domain.App.LangStr>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}