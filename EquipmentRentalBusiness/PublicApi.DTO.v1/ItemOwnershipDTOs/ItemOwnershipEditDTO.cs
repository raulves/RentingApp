using System;

namespace PublicApi.DTO.v1.ItemOwnershipDTOs
{
    public class ItemOwnershipEditDTO
    {
        public Guid Id { get; set; }
        
        public Guid? AppUserId { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid ItemId { get; set; }
    }
}