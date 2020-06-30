using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class BookingBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public string BookingNumber { get; set; } = default!;
        
        public DateTime BookingDate { get; set; }
        
        public DateTime BookingStartDay { get; set; }
        
        public DateTime BookingEndDay { get; set; }
        
        public int BookingPeriodDays { get; set; }
        
        public decimal PricePerDay { get; set; }
        
        public decimal VatPercent { get; set; }
        
        public decimal Vat { get; set; }
        
        public decimal BookingWithoutVat { get; set; }
        
        public decimal BookingTotal { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        public ItemBLL? Item { get; set; }
        
        [ForeignKey(nameof(ItemOwner))]
        public Guid? ItemOwnerId { get; set; }
        public AppUserBLL? ItemOwner { get; set; }
        
        [ForeignKey(nameof(Renter))]
        public Guid? RenterId { get; set; }
        public AppUserBLL? Renter { get; set; }
        
        [ForeignKey(nameof(ItemOwnerCompany))]
        public Guid? ItemOwnerCompanyId { get; set; }
        public CompanyBLL? ItemOwnerCompany { get; set; }
        
        [ForeignKey(nameof(RenterCompany))]
        public Guid? RenterCompanyId { get; set; }
        public CompanyBLL? RenterCompany { get; set; }
        
        public Guid? InvoiceId { get; set; }
        public InvoiceBLL? Invoice { get; set; }

        public ICollection<ItemBookedBLL>? ItemsBooked { get; set; }
    }
    
}