using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

#pragma warning disable 1591
namespace WebApp.ViewModels.Identity
{
    public class AppUserViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Email), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string Email { get; set; } = default!;
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(UserName), ResourceType = typeof(Resources.Views.Account.AppUser.AppUser))]
        public string UserName { get; set; } = default!;
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string FirstName { get; set; } = default!;

        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string LastName { get; set; } = default!;
        
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        public string? Phone { get; set; }
        
        public string FullName => FirstName + " " + LastName;
        
        public Guid? LocationId { get; set; }
        public LocationCreateEditViewModel? Location { get; set; }

        public ICollection<CompanyCreateEditViewModel>? Companies { get; set; }

        public ICollection<ImageCreateEditViewModel>? Images { get; set; }

        public ICollection<ItemCreateEditViewModel>? Items { get; set; }

        public ICollection<ItemsBookedCreateEditViewModel>? ItemsBooked { get; set; }

        public ICollection<ItemCategoriesCreateEditViewModel>? ItemCategories { get; set; }

        public ICollection<ItemDescriptionCreateEditViewModel>? ItemDescriptions { get; set; }
        
        public ICollection<ItemOwnershipCreateEditViewModel>? ItemOwnerships { get; set; }
        
        public ICollection<PaymentCreateEditViewModel>? Payments { get; set; }

        public ICollection<PriceCreateEditViewModel>? Prices { get; set; }

        public ICollection<AppUserCompanyCreateEditViewModel>? AppUserCompanies { get; set; }

        public ICollection<InvoiceCreateEditViewModel>? Invoices { get; set; }
        
        public ICollection<BookingsCreateEditViewModel>? BookingsAsItemOwner { get; set; }
        
        public ICollection<BookingsCreateEditViewModel>? BookingsAsRenter { get; set; }
        
        public ICollection<BookingsCreateEditViewModel>? Bookings { get; set; }
        
        public ICollection<LocationCreateEditViewModel>? Locations { get; set; }
    }
}