using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using PublicApi.DTO.v1.AppUserCompanyDTOs;
using PublicApi.DTO.v1.BookingDTOs;
using PublicApi.DTO.v1.CategoryDTOs;
using PublicApi.DTO.v1.CompanyDTOs;
using PublicApi.DTO.v1.Identity;
using PublicApi.DTO.v1.ImageDTOs;
using PublicApi.DTO.v1.InvoiceDTOs;
using PublicApi.DTO.v1.ItemBookedDTOs;
using PublicApi.DTO.v1.ItemCategoryDTOs;
using PublicApi.DTO.v1.ItemDescriptionDTOs;
using PublicApi.DTO.v1.ItemDTOs;
using PublicApi.DTO.v1.ItemOwnershipDTOs;
using PublicApi.DTO.v1.LocationDTOs;
using PublicApi.DTO.v1.PaymentDTOs;
using PublicApi.DTO.v1.PriceDTOs;
using PublicApi.DTO.v1.ProductDescriptionDTOs;
using PublicApi.DTO.v1.RentalPeriodDTOs;

#pragma warning disable 1591
namespace PublicApi.DTO.v1.Mappers
{
    public class DTOMapper<TLeftObject, TRightObject> : BaseDTOMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DTOMapper() : base()
        {
            // add more mappings
            MapperConfigurationExpression.CreateMap<AppRoleBLL, AppRoleDTO>();
            MapperConfigurationExpression.CreateMap<AppRoleDTO, AppRoleBLL>();
            
            MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserDTO>();
            MapperConfigurationExpression.CreateMap<AppUserDTO, AppUserBLL>();
            
            MapperConfigurationExpression.CreateMap<AppUserCompanyBLL, AppUserCompanyDTO>();
            MapperConfigurationExpression.CreateMap<AppUserCompanyDTO, AppUserCompanyBLL>();
            
            MapperConfigurationExpression.CreateMap<BookingBLL, BookingDTO>();
            MapperConfigurationExpression.CreateMap<BookingDTO, BookingBLL>();
            
            MapperConfigurationExpression.CreateMap<CategoryBLL, CategoryDTO>();
            MapperConfigurationExpression.CreateMap<CategoryDTO, CategoryBLL>();
            
            MapperConfigurationExpression.CreateMap<CompanyBLL, CompanyDTO>();
            MapperConfigurationExpression.CreateMap<CompanyDTO, CompanyBLL>();
            
            MapperConfigurationExpression.CreateMap<ImageBLL, ImageDTO>();
            MapperConfigurationExpression.CreateMap<ImageDTO, ImageBLL>();
            
            MapperConfigurationExpression.CreateMap<InvoiceBLL, InvoiceDTO>();
            MapperConfigurationExpression.CreateMap<InvoiceDTO, InvoiceBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemBLL, ItemDTO>();
            MapperConfigurationExpression.CreateMap<ItemDTO, ItemBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemBookedBLL, ItemBookedDTO>();
            MapperConfigurationExpression.CreateMap<ItemBookedDTO, ItemBookedBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemCategoryBLL, ItemCategoryDTO>();
            MapperConfigurationExpression.CreateMap<ItemCategoryDTO, ItemCategoryBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemDescriptionBLL, ItemDescriptionDTO>();
            MapperConfigurationExpression.CreateMap<ItemDescriptionDTO, ItemDescriptionBLL>();
            
            MapperConfigurationExpression.CreateMap<ItemOwnershipBLL, ItemOwnershipDTO>();
            MapperConfigurationExpression.CreateMap<ItemOwnershipDTO, ItemOwnershipBLL>();
            
            MapperConfigurationExpression.CreateMap<LocationBLL, LocationDTO>();
            MapperConfigurationExpression.CreateMap<LocationDTO, LocationBLL>();
            
            MapperConfigurationExpression.CreateMap<PaymentBLL, PaymentDTO>();
            MapperConfigurationExpression.CreateMap<PaymentDTO, PaymentBLL>();

            MapperConfigurationExpression.CreateMap<PriceBLL, PriceDTO>();
            MapperConfigurationExpression.CreateMap<PriceDTO, PriceBLL>();
            
            MapperConfigurationExpression.CreateMap<ProductDescriptionBLL, ProductDescriptionDTO>();
            MapperConfigurationExpression.CreateMap<ProductDescriptionDTO, ProductDescriptionBLL>();
            
            MapperConfigurationExpression.CreateMap<RentalPeriodBLL, RentalPeriodDTO>();
            MapperConfigurationExpression.CreateMap<RentalPeriodDTO, RentalPeriodBLL>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}