using System;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.ItemDescriptionDTOs
{
    public class ItemDescriptionDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public Guid ProductDescriptionId { get; set; }

        public Guid ItemId { get; set; }
    }
}