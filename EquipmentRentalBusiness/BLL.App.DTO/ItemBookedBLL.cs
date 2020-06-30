using System;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ItemBookedBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
        
        public Guid ItemId { get; set; }
        public ItemBLL? Item { get; set; }
        
        public Guid BookingId { get; set; } = default!;
        public BookingBLL? Booking { get; set; }
    }
    
}