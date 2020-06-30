#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.Identity;

namespace WebApp.ViewModels
{
    public class ItemCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserViewModel? AppUser { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Description { get; set; } = default!;
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Brand), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Brand { get; set; } = default!;
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Display(Name = nameof(Model), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Model { get; set; } = default!;

        public ICollection<ItemCategoriesCreateEditViewModel>? ItemCategories { get; set; }

        [Display(Name = nameof(Images), ResourceType = typeof(Resources.Domain.Item.Item))]
        public ICollection<ImageCreateEditViewModel>? Images { get; set; }

        public ICollection<ItemOwnershipCreateEditViewModel>? ItemOwnerships { get; set; }
        
        [Display(Name = nameof(LocationId), ResourceType = typeof(Resources.Domain.Item.Item))]
        public Guid LocationId { get; set; } = default!;
        public LocationCreateEditViewModel? Location { get; set; }
        
        public ICollection<BookingsCreateEditViewModel>? Bookings { get; set; }

        public ICollection<ItemsBookedCreateEditViewModel>? ItemsBooked { get; set; }

        public ICollection<PriceCreateEditViewModel>? Prices { get; set; }

        public ICollection<ItemDescriptionCreateEditViewModel>? ItemDescriptions { get; set; }

        public SelectList? LocationSelectList { get; set; }

        [Display(Name = nameof(CategoryId), ResourceType = typeof(Resources.Domain.Item.Item))]
        public Guid CategoryId { get; set; } = default!;
        public SelectList? CategorySelectList { get; set; }
        
        [Display(Name = nameof(ItemDayPrice), ResourceType = typeof(Resources.Domain.Item.Item))]
        public decimal ItemDayPrice { get; set; }
        
        [Display(Name = nameof(ItemWeekPrice), ResourceType = typeof(Resources.Domain.Item.Item))]
        public decimal ItemWeekPrice { get; set; }
        
        [Display(Name = nameof(ItemMonthPrice), ResourceType = typeof(Resources.Domain.Item.Item))]
        public decimal ItemMonthPrice { get; set; }
        
        [Display(Name = nameof(CompanyId), ResourceType = typeof(Resources.Domain.Item.Item))]
        public Guid? CompanyId { get; set; }
        public SelectList? CompanySelectList { get; set; }
        
        public SelectList? ProductDescriptionSelectList { get; set; }

        // Adding item descriptions
        // Quick fix for first product description, multiple problem
        public string? ItemDescription { get; set; }
        
        [Display(Name = nameof(ProductDescriptionId), ResourceType = typeof(Resources.Domain.Item.Item))]
        public Guid ProductDescriptionId { get; set; }
        
        public List<string>? Descriptions { get; set; } // input väljalt tulevad väärtused
        public List<Guid>? ProductDescriptionIds { get; set; }

    }
}