using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Item : DomainEntityIdMetadataUser<AppUser>
    {
        [MinLength(1)]
        [MaxLength(64)]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Description { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Display(Name = nameof(Brand), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Brand { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Display(Name = nameof(Model), ResourceType = typeof(Resources.Domain.Item.Item))]
        public string Model { get; set; } = default!;

        public ICollection<ItemCategory>? ItemCategories { get; set; }
        
        public ICollection<Image>? Images { get; set; }

        public ICollection<ItemOwnership>? ItemOwnerships { get; set; }
        
        [Display(Name = nameof(LocationId), ResourceType = typeof(Resources.Domain.Item.Item))]
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }
        
        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<ItemBooked>? ItemsBooked { get; set; }

        public ICollection<Price>? Prices { get; set; }

        public ICollection<ItemDescription>? ItemDescriptions { get; set; }
    }
    
}