using System;

namespace PublicApi.DTO.v1.PriceDTOs
{
    public class PriceEditDTO
    {
        public Guid Id { get; set; }
        
        public decimal ItemPrice { get; set; }
        
        public Guid ItemId { get; set; }

        public Guid RentalPeriodId { get; set; }
    }
}