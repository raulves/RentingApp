using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{

    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string FirstName { get; set; } = default!;

        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public string LastName { get; set; } = default!;
        
        [MaxLength(64)]
        [MinLength(1)]
        public string? Phone { get; set; }
        
        public string FullName => FirstName + " " + LastName;
        
        
        public Guid? LocationId { get; set; }
        public Location? Location { get; set; }

        public ICollection<Company>? Companies { get; set; }

        public ICollection<Image>? Images { get; set; }

        public ICollection<Item>? Items { get; set; }

        public ICollection<ItemBooked>? ItemsBooked { get; set; }

        public ICollection<ItemCategory>? ItemCategories { get; set; }

        public ICollection<ItemDescription>? ItemDescriptions { get; set; }
        
        public ICollection<ItemOwnership>? ItemOwnerships { get; set; }
        
        public ICollection<Payment>? Payments { get; set; }

        public ICollection<Price>? Prices { get; set; }

        public ICollection<AppUserCompany>? AppUserCompanies { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }
        
        [InverseProperty(nameof(Booking.ItemOwner))]
        public ICollection<Booking>? BookingsAsItemOwner { get; set; }
        
        [InverseProperty(nameof(Booking.Renter))]
        public ICollection<Booking>? BookingsAsRenter { get; set; }

        // AppUser all bookings (as a renter or as a owner)
        // , including also appUser companies bookings
        [InverseProperty(nameof(AppUser))]
        public ICollection<Booking>? Bookings { get; set; }
        
        [InverseProperty(nameof(App.Location.AppUser))]
        public ICollection<Location>? Locations { get; set; }
        
        
    }
    
    
}