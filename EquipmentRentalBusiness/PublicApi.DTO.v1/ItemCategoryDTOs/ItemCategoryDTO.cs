using System;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.ItemCategoryDTOs
{
    public class ItemCategoryDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public Guid CategoryId { get; set; }

        public Guid ItemId { get; set; }
    }
}