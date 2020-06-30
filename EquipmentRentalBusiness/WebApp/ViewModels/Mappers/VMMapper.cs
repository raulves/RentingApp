#pragma warning disable 1591
using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using Domain.App;
using Domain.App.Identity;
using WebApp.ViewModels.Identity;

namespace WebApp.ViewModels.Mappers
{
    public class VMMapper<TLeftObject, TRightObject> : BaseVMMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public VMMapper() : base()
        {
            // add more mappings

            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemView, ItemViewVM>();
            MapperConfigurationExpression.CreateMap<ItemViewVM, BLL.App.DTO.ItemView>();
            
            MapperConfigurationExpression.CreateMap<ItemBLL, ItemViewProfileVM>();
            MapperConfigurationExpression.CreateMap<ItemViewProfileVM, ItemBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemBLL, ItemEditVM>();
            MapperConfigurationExpression.CreateMap<ItemEditVM, ItemBLL>();
            
            MapperConfigurationExpression.CreateMap<AppRoleBLL, AppRoleViewModel>();
            MapperConfigurationExpression.CreateMap<AppRoleViewModel, AppRoleBLL>();
            
            MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserViewModel>();
            MapperConfigurationExpression.CreateMap<AppUserViewModel, AppUserBLL>();
            
            MapperConfigurationExpression.CreateMap<AppUserCompanyBLL, AppUserCompanyCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<AppUserCompanyCreateEditViewModel, AppUserCompanyBLL>();
            
            MapperConfigurationExpression.CreateMap<BookingBLL, BookingsCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<BookingsCreateEditViewModel, BookingBLL>();
            
            MapperConfigurationExpression.CreateMap<CategoryBLL, CategoryCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<CategoryCreateEditViewModel, CategoryBLL>();
            
            MapperConfigurationExpression.CreateMap<CompanyBLL, CompanyCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<CompanyCreateEditViewModel, CompanyBLL>();

            MapperConfigurationExpression.CreateMap<ImageBLL, ImageCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ImageCreateEditViewModel, ImageBLL>();
            
            MapperConfigurationExpression.CreateMap<InvoiceBLL, InvoiceCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<InvoiceCreateEditViewModel, InvoiceBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemBLL, ItemCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ItemCreateEditViewModel, ItemBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemBookedBLL, ItemsBookedCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ItemsBookedCreateEditViewModel, ItemBookedBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemCategoryBLL, ItemCategoriesCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ItemCategoriesCreateEditViewModel, ItemCategoryBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemDescriptionBLL, ItemDescriptionCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ItemDescriptionCreateEditViewModel, ItemDescriptionBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemOwnershipBLL, ItemOwnershipCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ItemOwnershipCreateEditViewModel, ItemOwnershipBLL>();
            
            MapperConfigurationExpression.CreateMap<LocationBLL, LocationCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<LocationCreateEditViewModel, LocationBLL>();
            
            MapperConfigurationExpression.CreateMap<PaymentBLL, PaymentCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<PaymentCreateEditViewModel, PaymentBLL>();

            MapperConfigurationExpression.CreateMap<PriceBLL, PriceCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<PriceCreateEditViewModel, PriceBLL>();
            
            MapperConfigurationExpression.CreateMap<ProductDescriptionBLL, ProductDescriptionCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ProductDescriptionCreateEditViewModel, ProductDescriptionBLL>();
            
            MapperConfigurationExpression.CreateMap<RentalPeriodBLL, RentalPeriodCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<RentalPeriodCreateEditViewModel, RentalPeriodBLL>();
            
            MapperConfigurationExpression.CreateMap<SingleItemView, SingleItemViewVM>();
            MapperConfigurationExpression.CreateMap<SingleItemViewVM, SingleItemView>();
            
            MapperConfigurationExpression.CreateMap<AppUser, AppUserViewModel>();
            MapperConfigurationExpression.CreateMap<AppUserViewModel, AppUser>();
            
            MapperConfigurationExpression.CreateMap<AppUser, AppUserCreateEditVM>();
            MapperConfigurationExpression.CreateMap<AppUserCreateEditVM, AppUser>();
            
            MapperConfigurationExpression.CreateMap<AppUser, AppUserDeleteVM>();
            MapperConfigurationExpression.CreateMap<AppUserDeleteVM, AppUser>();
            
            MapperConfigurationExpression.CreateMap<AppRole, AppRoleViewModel>();
            MapperConfigurationExpression.CreateMap<AppRoleViewModel, AppRole>();
            
            MapperConfigurationExpression.CreateMap<ItemBLL, ItemAdminVM>();
            MapperConfigurationExpression.CreateMap<ItemAdminVM, ItemBLL>();
            
            MapperConfigurationExpression.CreateMap<Booking, BookingsCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<BookingsCreateEditViewModel, Booking>();
            
            MapperConfigurationExpression.CreateMap<Item, ItemCreateEditViewModel>();
            MapperConfigurationExpression.CreateMap<ItemCreateEditViewModel, Item>();
            
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.LangStrTranslation, LangStrTranslationViewModel>();
            MapperConfigurationExpression.CreateMap<LangStrTranslationViewModel, BLL.App.DTO.LangStrTranslation>();
            
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.LangStr, LangStrViewModel>();
            MapperConfigurationExpression.CreateMap<LangStrViewModel, BLL.App.DTO.LangStr>();
            
            MapperConfigurationExpression.CreateMap<LangStrIndexBLL, LangStrIndexViewModel>();
            MapperConfigurationExpression.CreateMap<LangStrIndexViewModel, LangStrIndexBLL>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}