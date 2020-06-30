using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class CompanyBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public string CompanyName { get; set; } = default!;
        
        public string RegisterCode { get; set; } = default!;
        
        public string? VatNumber { get; set; }
        
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public Guid LocationId { get; set; } = default!;
        public LocationBLL? Location { get; set; }
        
        public ICollection<ItemOwnershipBLL>? ItemOwnerships { get; set; }

        public ICollection<AppUserCompanyBLL>? AppUserCompanies { get; set; }
        
        public ICollection<PaymentBLL>? Payments { get; set; }
        
        [InverseProperty(nameof(BookingBLL.ItemOwnerCompany))]
        public ICollection<BookingBLL>? BookingsAsItemOwner { get; set; }
        
        [InverseProperty(nameof(BookingBLL.RenterCompany))]
        public ICollection<BookingBLL>? BookingsAsRenter { get; set; }

        public ICollection<InvoiceBLL>? Invoices { get; set; }
    }
    
}