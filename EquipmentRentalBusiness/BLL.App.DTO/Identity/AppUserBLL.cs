using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO.Identity
{
    public class AppUserBLL : IDomainEntityId
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
        public LocationBLL? Location { get; set; }

        public ICollection<CompanyBLL>? Companies { get; set; }

        public ICollection<ImageBLL>? Images { get; set; }

        public ICollection<ItemBLL>? Items { get; set; }

        public ICollection<ItemBookedBLL>? ItemsBooked { get; set; }

        public ICollection<ItemCategoryBLL>? ItemCategories { get; set; }

        public ICollection<ItemDescriptionBLL>? ItemDescriptions { get; set; }
        
        public ICollection<ItemOwnershipBLL>? ItemOwnerships { get; set; }
        
        public ICollection<PaymentBLL>? Payments { get; set; }

        public ICollection<PriceBLL>? Prices { get; set; }

        public ICollection<AppUserCompanyBLL>? AppUserCompanies { get; set; }

        public ICollection<InvoiceBLL>? Invoices { get; set; }
        
        [InverseProperty(nameof(BookingBLL.ItemOwner))]
        public ICollection<BookingBLL>? BookingsAsItemOwner { get; set; }
        
        [InverseProperty(nameof(BookingBLL.Renter))]
        public ICollection<BookingBLL>? BookingsAsRenter { get; set; }
        
        [InverseProperty(nameof(AppUserBLL))]
        public ICollection<BookingBLL>? Bookings { get; set; }
        
        [InverseProperty(nameof(LocationBLL.AppUser))]
        public ICollection<LocationBLL>? Locations { get; set; }
    }

}