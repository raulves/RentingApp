using System;

namespace PublicApi.DTO.v1.ItemBookedDTOs
{
    public class ItemBookedEditDTO
    {
        public Guid Id { get; set; }
        
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        
        public Guid ItemId { get; set; }

        public Guid BookingId { get; set; }
    }
}