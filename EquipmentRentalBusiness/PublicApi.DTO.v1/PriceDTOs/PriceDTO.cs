using System;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.PriceDTOs
{
    public class PriceDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public decimal ItemPrice { get; set; }
        
        public Guid ItemId { get; set; }

        public Guid RentalPeriodId { get; set; }
    }
}