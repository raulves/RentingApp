using System;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.ItemBookedDTOs
{
    public class ItemBookedDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        
        public Guid ItemId { get; set; }

        public Guid BookingId { get; set; }
    }
}