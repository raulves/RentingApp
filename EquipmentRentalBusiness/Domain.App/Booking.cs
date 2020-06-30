using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Booking : DomainEntityIdMetadataUser<AppUser>
    {
        
        [MinLength(1)]
        [MaxLength(64)]
        public string BookingNumber { get; set; } = default!;
        
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = nameof(BookingStartDay), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public DateTime BookingStartDay { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = nameof(BookingEndDay), ResourceType = typeof(Resources.Domain.Booking.Booking))]
        public DateTime BookingEndDay { get; set; }
        
        public int BookingPeriodDays { get; set; }
        
        public decimal PricePerDay { get; set; }
        
        public decimal VatPercent { get; set; }
        
        public decimal Vat { get; set; }
        
        public decimal BookingWithoutVat { get; set; }
        
        public decimal BookingTotal { get; set; }
        
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        
        [ForeignKey(nameof(ItemOwner))]
        public Guid? ItemOwnerId { get; set; }
        public AppUser? ItemOwner { get; set; }
        
        [ForeignKey(nameof(Renter))]
        public Guid? RenterId { get; set; }
        public AppUser? Renter { get; set; }
        
        [ForeignKey(nameof(ItemOwnerCompany))]
        public Guid? ItemOwnerCompanyId { get; set; }
        public Company? ItemOwnerCompany { get; set; }
        
        [ForeignKey(nameof(RenterCompany))]
        public Guid? RenterCompanyId { get; set; }
        public Company? RenterCompany { get; set; }
        
        public Guid? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public ICollection<ItemBooked>? ItemsBooked { get; set; }
    }
    
    
}