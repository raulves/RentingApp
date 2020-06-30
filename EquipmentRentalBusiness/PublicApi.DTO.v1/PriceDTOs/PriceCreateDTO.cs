using System;

namespace PublicApi.DTO.v1.PriceDTOs
{
    public class PriceCreateDTO
    {
        public decimal ItemPrice { get; set; }
        
        public Guid ItemId { get; set; }

        public Guid RentalPeriodId { get; set; }
    }
}