using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class BookingDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
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
        [JsonIgnore]
        public ItemDAL? Item { get; set; }
        
        [ForeignKey(nameof(ItemOwner))]
        public Guid? ItemOwnerId { get; set; }
        [JsonIgnore]
        public AppUserDAL? ItemOwner { get; set; }
        
        [ForeignKey(nameof(Renter))]
        public Guid? RenterId { get; set; }
        [JsonIgnore]
        public AppUserDAL? Renter { get; set; }
        
        [ForeignKey(nameof(ItemOwnerCompany))]
        public Guid? ItemOwnerCompanyId { get; set; }
        [JsonIgnore]
        public CompanyDAL? ItemOwnerCompany { get; set; }
        
        [ForeignKey(nameof(RenterCompany))]
        public Guid? RenterCompanyId { get; set; }
        [JsonIgnore]
        public CompanyDAL? RenterCompany { get; set; }
        
        public Guid? InvoiceId { get; set; }
        [JsonIgnore]
        public InvoiceDAL? Invoice { get; set; }
        

        [JsonIgnore]
        public ICollection<ItemBookedDAL>? ItemsBooked { get; set; }
    }
    
}