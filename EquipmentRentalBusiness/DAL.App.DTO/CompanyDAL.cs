using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class CompanyDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public string CompanyName { get; set; } = default!;
        
        public string RegisterCode { get; set; } = default!;
        
        public string? VatNumber { get; set; }
        
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public Guid LocationId { get; set; } = default!;
        public LocationDAL? Location { get; set; }
        
        [JsonIgnore]
        public ICollection<ItemOwnershipDAL>? ItemOwnerships { get; set; }

        [JsonIgnore]
        public ICollection<AppUserCompanyDAL>? AppUserCompanies { get; set; }
        
        [JsonIgnore]
        public ICollection<PaymentDAL>? Payments { get; set; }
        
        [JsonIgnore]
        [InverseProperty(nameof(BookingDAL.ItemOwnerCompany))]
        public ICollection<BookingDAL>? BookingsAsItemOwner { get; set; }
        
        [JsonIgnore]
        [InverseProperty(nameof(BookingDAL.RenterCompany))]
        public ICollection<BookingDAL>? BookingsAsRenter { get; set; }

        [JsonIgnore]
        public ICollection<InvoiceDAL>? Invoices { get; set; }
    }
    
}