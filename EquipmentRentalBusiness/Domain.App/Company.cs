using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Company : DomainEntityIdMetadataUser<AppUser>
    {
        [MinLength(1)]
        [MaxLength(64)] 
        public string CompanyName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        public string RegisterCode { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        public string? VatNumber { get; set; }
        
        [MinLength(1)]
        [MaxLength(64)]
        public string? Email { get; set; }
        
        [MinLength(1)]
        [MaxLength(64)]
        public string? Phone { get; set; }
        
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }
        
        public ICollection<ItemOwnership>? ItemOwnerships { get; set; }

        public ICollection<AppUserCompany>? AppUserCompanies { get; set; }
        
        public ICollection<Payment>? Payments { get; set; }
        
        [InverseProperty(nameof(Booking.ItemOwnerCompany))]
        public ICollection<Booking>? BookingsAsItemOwner { get; set; }
        
        [InverseProperty(nameof(Booking.RenterCompany))]
        public ICollection<Booking>? BookingsAsRenter { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }
        
    }
    
}