#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.Identity;

namespace WebApp.ViewModels
{
    public class BookingsCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public AppUserViewModel? AppUser { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Views.Shared.Common))]
        public string BookingNumber { get; set; } = default!;
        
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = nameof(BookingStartDay), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public DateTime BookingStartDay { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = nameof(BookingEndDay), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public DateTime BookingEndDay { get; set; }
        
        [Display(Name = nameof(BookingPeriodDays), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public int BookingPeriodDays { get; set; }
        
        [Display(Name = nameof(PricePerDay), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public decimal PricePerDay { get; set; }
        public decimal VatPercent { get; set; }
        public decimal Vat { get; set; }
        
        [Display(Name = nameof(BookingWithoutVat), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public decimal BookingWithoutVat { get; set; }
        
        [Display(Name = nameof(BookingTotal), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public decimal BookingTotal { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public ItemCreateEditViewModel? Item { get; set; }
        
        public Guid? ItemOwnerId { get; set; }
        public AppUserViewModel? ItemOwner { get; set; }
        
        public Guid? RenterId { get; set; }
        public AppUserViewModel? Renter { get; set; }
        
        public Guid? ItemOwnerCompanyId { get; set; }
        public CompanyCreateEditViewModel? ItemOwnerCompany { get; set; }
        
        public Guid? RenterCompanyId { get; set; }
        public CompanyCreateEditViewModel? RenterCompany { get; set; }
        
        public Guid? InvoiceId { get; set; }
        public InvoiceCreateEditViewModel? Invoice { get; set; }
        

        public ICollection<ItemsBookedCreateEditViewModel>? ItemsBooked { get; set; }

        public SelectList? InvoiceSelectList { get; set; }
        public SelectList? ItemSelectList { get; set; }
        public SelectList? ItemOwnerSelectList { get; set; }
        public SelectList? ItemOwnerCompanySelectList { get; set; }
        public SelectList? RentalPeriodSelectList { get; set; }
        public SelectList? RenterSelectList { get; set; }
        public SelectList? RenterCompanySelectList { get; set; }
        
    }
}