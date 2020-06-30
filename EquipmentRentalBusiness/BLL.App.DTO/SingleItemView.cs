using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class SingleItemView : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public AppUserBLL? AppUser { get; set; }
        
        public string Description { get; set; } = default!;
        
        public string Brand { get; set; } = default!;
        
        public string Model { get; set; } = default!;

        public decimal PricePerDay { get; set; }
        public decimal PricePerWeek { get; set; }
        public decimal PricePerMonth { get; set; }
        
        [DataType(DataType.Date)]
        [Required]
        public DateTime BookingStartDay { get; set; }
        
        [DataType(DataType.Date)]
        [Required]
        public DateTime BookingEndDay { get; set; }

        public ICollection<ItemCategoryBLL>? ItemCategories { get; set; }
        
        public ICollection<ImageBLL>? Images { get; set; }

        public Guid LocationId { get; set; } = default!;
        
        public LocationBLL? Location { get; set; }
        
        public ICollection<BookingBLL>? Bookings { get; set; }
        
        public ICollection<ItemBookedBLL>? ItemsBooked { get; set; }
        
        public ICollection<ItemDescriptionBLL>? ItemDescriptions { get; set; }
        
        public Guid? CompanyId { get; set; }
        
        public Guid? ItemOwnerCompanyId { get; set; }
        public CompanyBLL? ItemOwnerCompany { get; set; }
        
        public bool HasVatNumber { get; set; }
    }
}