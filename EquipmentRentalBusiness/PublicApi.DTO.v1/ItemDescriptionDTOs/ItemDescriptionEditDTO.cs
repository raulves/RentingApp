using System;

namespace PublicApi.DTO.v1.ItemDescriptionDTOs
{
    public class ItemDescriptionEditDTO
    {
        public Guid Id { get; set; }
        
        public Guid ProductDescriptionId { get; set; }

        public Guid ItemId { get; set; }
    }
}