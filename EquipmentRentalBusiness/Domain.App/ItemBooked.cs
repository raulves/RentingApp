using System;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class ItemBooked : DomainEntityIdMetadataUser<AppUser>
    {
        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
        
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        
        public Guid BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
    
}