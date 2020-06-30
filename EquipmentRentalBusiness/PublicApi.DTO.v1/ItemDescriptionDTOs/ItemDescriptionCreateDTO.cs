using System;

namespace PublicApi.DTO.v1.ItemDescriptionDTOs
{
    public class ItemDescriptionCreateDTO
    {
        public Guid ProductDescriptionId { get; set; }

        public Guid ItemId { get; set; }
    }
}