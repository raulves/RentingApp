using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using PublicApi.DTO.v1.ItemCategoryDTOs;
using WebApp.ViewModels.Identity;

#pragma warning disable 1591

namespace WebApp.ViewModels
{
    public class SingleItemViewVM : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(AppUserId), ResourceType = typeof(Resources.Views.Bookings.Index.Index))]
        public Guid AppUserId { get; set; }
        
        public AppUserViewModel? AppUser { get; set; }
        
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Description { get; set; } = default!;
        
        [Display(Name = nameof(Brand), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Brand { get; set; } = default!;
        
        [Display(Name = nameof(Model), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Model { get; set; } = default!;

        [Display(Name = nameof(PricePerDay), ResourceType = typeof(Resources.Domain.Item.Item))]
        public decimal PricePerDay { get; set; }
        
        [Display(Name = nameof(PricePerWeek), ResourceType = typeof(Resources.Domain.Item.Item))]
        public decimal PricePerWeek { get; set; }
        
        [Display(Name = nameof(PricePerMonth), ResourceType = typeof(Resources.Domain.Item.Item))]
        public decimal PricePerMonth { get; set; }
        
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = nameof(BookingStartDay), ResourceType = typeof(Resources.Domain.Item.Item))]
        public DateTime BookingStartDay { get; set; }
        
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = nameof(BookingEndDay), ResourceType = typeof(Resources.Domain.Item.Item))]
        public DateTime BookingEndDay { get; set; }

        public ICollection<ItemCategoriesCreateEditViewModel>? ItemCategories { get; set; }
        
        public ICollection<ImageCreateEditViewModel>? Images { get; set; }

        [Display(Name = nameof(LocationId), ResourceType = typeof(Resources.Domain.Item.Item))]
        public Guid LocationId { get; set; } = default!;
        
        public LocationCreateEditViewModel? Location { get; set; }
        
        public ICollection<BookingsCreateEditViewModel>? Bookings { get; set; }
        
        public ICollection<ItemsBookedCreateEditViewModel>? ItemsBooked { get; set; }
        
        public ICollection<ItemDescriptionCreateEditViewModel>? ItemDescriptions { get; set; }

        [Display(Name = nameof(CompanyId), ResourceType = typeof(Resources.Domain.Item.Item))]
        public Guid? CompanyId { get; set; }
        public SelectList? CompanySelectList { get; set; }
        public CompanyCreateEditViewModel? ItemOwnerCompany { get; set; }
    }
}