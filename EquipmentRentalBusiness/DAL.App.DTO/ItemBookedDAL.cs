using System;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ItemBookedDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
        
        public Guid ItemId { get; set; } = default!;
        [JsonIgnore]
        public ItemDAL? Item { get; set; }
        
        public Guid BookingId { get; set; } = default!;
        [JsonIgnore]
        public BookingDAL? Booking { get; set; }
    }
    
}