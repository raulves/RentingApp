using System;

namespace PublicApi.DTO.v1.ItemOwnershipDTOs
{
    public class ItemOwnershipCreateDTO
    {
        public Guid? AppUserId { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid ItemId { get; set; }
    }
}