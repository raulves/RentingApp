using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ItemBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUserBLL? AppUser { get; set; }
        
        public string Description { get; set; } = default!;
        
        public string Brand { get; set; } = default!;
        
        public string Model { get; set; } = default!;

        public ICollection<ItemCategoryBLL>? ItemCategories { get; set; }

        public ICollection<ImageBLL>? Images { get; set; }

        public ICollection<ItemOwnershipBLL>? ItemOwnerships { get; set; }
        
        public Guid LocationId { get; set; }
        public LocationBLL? Location { get; set; }
        
        public ICollection<BookingBLL>? Bookings { get; set; }

        public ICollection<ItemBookedBLL>? ItemsBooked { get; set; }

        public ICollection<PriceBLL>? Prices { get; set; }

        public ICollection<ItemDescriptionBLL>? ItemDescriptions { get; set; }
    }
    
}