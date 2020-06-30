using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppUserDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; } = default!;
        
        [MaxLength(64)]
        [MinLength(1)]
        public string? Phone { get; set; }
        
        public string FullName => FirstName + " " + LastName;
        
        public Guid? LocationId { get; set; }
        public LocationDAL? Location { get; set; }

        public ICollection<CompanyDAL>? Companies { get; set; }

        public ICollection<ImageDAL>? Images { get; set; }

        public ICollection<ItemDAL>? Items { get; set; }

        public ICollection<ItemBookedDAL>? ItemsBooked { get; set; }

        public ICollection<ItemCategoryDAL>? ItemCategories { get; set; }

        public ICollection<ItemDescriptionDAL>? ItemDescriptions { get; set; }
        
        public ICollection<ItemOwnershipDAL>? ItemOwnerships { get; set; }
        
        public ICollection<PaymentDAL>? Payments { get; set; }

        public ICollection<PriceDAL>? Prices { get; set; }

        public ICollection<AppUserCompanyDAL>? AppUserCompanies { get; set; }

        public ICollection<InvoiceDAL>? Invoices { get; set; }
        
        [InverseProperty(nameof(BookingDAL.ItemOwner))]
        public ICollection<BookingDAL>? BookingsAsItemOwner { get; set; }
        
        [InverseProperty(nameof(BookingDAL.Renter))]
        public ICollection<BookingDAL>? BookingsAsRenter { get; set; }
        
        [InverseProperty(nameof(AppUserDAL))]
        public ICollection<BookingDAL>? Bookings { get; set; }
        
        [InverseProperty(nameof(LocationDAL.AppUser))]
        public ICollection<LocationDAL>? Locations { get; set; }
    }
    
}