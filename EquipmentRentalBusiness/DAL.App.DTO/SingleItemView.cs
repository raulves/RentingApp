using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class SingleItemView : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public AppUserDAL? AppUser { get; set; }
        
        public string Description { get; set; } = default!;
        
        public string Brand { get; set; } = default!;
        
        public string Model { get; set; } = default!;

        public decimal PricePerDay { get; set; }
        public decimal PricePerWeek { get; set; }
        public decimal PricePerMonth { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingStartDay { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BookingEndDay { get; set; }

        public ICollection<ItemCategoryDAL>? ItemCategories { get; set; }
        
        public ICollection<ImageDAL>? Images { get; set; }

        public Guid LocationId { get; set; } = default!;
        
        public LocationDAL? Location { get; set; }
        
        public ICollection<BookingDAL>? Bookings { get; set; }
        
        public ICollection<ItemBookedDAL>? ItemsBooked { get; set; }
        
        public ICollection<ItemDescriptionDAL>? ItemDescriptions { get; set; }
        public Guid? ItemOwnerCompanyId { get; set; }

        public CompanyDAL? ItemOwnerCompany { get; set; }

        public bool HasVatNumber { get; set; }
    }
}