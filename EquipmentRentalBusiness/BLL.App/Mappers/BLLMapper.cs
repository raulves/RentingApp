using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;

using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.Raul.Vesinurm.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseBLLMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemView, BLL.App.DTO.ItemView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemView, DAL.App.DTO.ItemView>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.SingleItemView, BLL.App.DTO.SingleItemView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.SingleItemView, DAL.App.DTO.SingleItemView>();
            
            MapperConfigurationExpression.CreateMap<AppRoleDAL, AppRoleBLL>();
            MapperConfigurationExpression.CreateMap<AppRoleBLL, AppRoleDAL>();
            
            MapperConfigurationExpression.CreateMap<AppUserDAL, AppUserBLL>();
            MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserDAL>();
            
            MapperConfigurationExpression.CreateMap<AppUserCompanyDAL, AppUserCompanyBLL>();
            MapperConfigurationExpression.CreateMap<AppUserCompanyBLL, AppUserCompanyDAL>();
            
            MapperConfigurationExpression.CreateMap<BookingDAL, BookingBLL>();
            MapperConfigurationExpression.CreateMap<BookingBLL, BookingDAL>();
            
            MapperConfigurationExpression.CreateMap<CategoryDAL, CategoryBLL>();
            MapperConfigurationExpression.CreateMap<CategoryBLL, CategoryDAL>();
            
            MapperConfigurationExpression.CreateMap<CompanyDAL, CompanyBLL>();
            MapperConfigurationExpression.CreateMap<CompanyBLL, CompanyDAL>();
            
            MapperConfigurationExpression.CreateMap<ImageDAL, ImageBLL>();
            MapperConfigurationExpression.CreateMap<ImageBLL, ImageDAL>();
            
            MapperConfigurationExpression.CreateMap<InvoiceDAL, InvoiceBLL>();
            MapperConfigurationExpression.CreateMap<InvoiceBLL, InvoiceDAL>();
            
            MapperConfigurationExpression.CreateMap<ItemDAL, ItemBLL>();
            MapperConfigurationExpression.CreateMap<ItemBLL, ItemDAL>();
            
            MapperConfigurationExpression.CreateMap<ItemBookedDAL, ItemBookedBLL>();
            MapperConfigurationExpression.CreateMap<ItemBookedBLL, ItemBookedDAL>();
            
            MapperConfigurationExpression.CreateMap<ItemCategoryDAL, ItemCategoryBLL>();
            MapperConfigurationExpression.CreateMap<ItemCategoryBLL, ItemCategoryDAL>();
            
            MapperConfigurationExpression.CreateMap<ItemDescriptionDAL, ItemDescriptionBLL>();
            MapperConfigurationExpression.CreateMap<ItemDescriptionBLL, ItemDescriptionDAL>();
            
            MapperConfigurationExpression.CreateMap<ItemOwnershipDAL, ItemOwnershipBLL>();
            MapperConfigurationExpression.CreateMap<ItemOwnershipBLL, ItemOwnershipDAL>();
            
            MapperConfigurationExpression.CreateMap<LocationDAL, LocationBLL>();
            MapperConfigurationExpression.CreateMap<LocationBLL, LocationDAL>();
            
            MapperConfigurationExpression.CreateMap<PaymentDAL, PaymentBLL>();
            MapperConfigurationExpression.CreateMap<PaymentBLL, PaymentDAL>();
            
            MapperConfigurationExpression.CreateMap<PaymentTypeDAL, PaymentTypeBLL>();
            MapperConfigurationExpression.CreateMap<PaymentTypeBLL, PaymentTypeDAL>();

            MapperConfigurationExpression.CreateMap<PriceDAL, PriceBLL>();
            MapperConfigurationExpression.CreateMap<PriceBLL, PriceDAL>();
            
            MapperConfigurationExpression.CreateMap<ProductDescriptionDAL, ProductDescriptionBLL>();
            MapperConfigurationExpression.CreateMap<ProductDescriptionBLL, ProductDescriptionDAL>();
            
            MapperConfigurationExpression.CreateMap<RentalPeriodDAL, RentalPeriodBLL>();
            MapperConfigurationExpression.CreateMap<RentalPeriodBLL, RentalPeriodDAL>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.LangStrTranslation, BLL.App.DTO.LangStrTranslation>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.LangStrTranslation, DAL.App.DTO.LangStrTranslation>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.LangStr, BLL.App.DTO.LangStr>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.LangStr, DAL.App.DTO.LangStr>();
            
            MapperConfigurationExpression.CreateMap<LangStrIndexDAL, LangStrIndexBLL>();
            MapperConfigurationExpression.CreateMap<LangStrIndexBLL, LangStrIndexDAL>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}