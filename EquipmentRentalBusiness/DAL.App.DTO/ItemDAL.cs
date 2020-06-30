using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ItemDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public string Description { get; set; } = default!;
        
        public string Brand { get; set; } = default!;
        
        public string Model { get; set; } = default!;

        [JsonIgnore]
        public ICollection<ItemCategoryDAL>? ItemCategories { get; set; }

        [JsonIgnore]
        public ICollection<ImageDAL>? Images { get; set; }

        [JsonIgnore]
        public ICollection<ItemOwnershipDAL>? ItemOwnerships { get; set; }
        
        public Guid LocationId { get; set; } = default!;
        [JsonIgnore]
        public LocationDAL? Location { get; set; }
        
        [JsonIgnore]
        public ICollection<BookingDAL>? Bookings { get; set; }

        [JsonIgnore]
        public ICollection<ItemBookedDAL>? ItemsBooked { get; set; }

        [JsonIgnore]
        public ICollection<PriceDAL>? Prices { get; set; }

        [JsonIgnore]
        public ICollection<ItemDescriptionDAL>? ItemDescriptions { get; set; }
    }
    
}