using System;

namespace PublicApi.DTO.v1.ItemCategoryDTOs
{
    public class ItemCategoryEditDTO
    {
        public Guid Id { get; set; }
        
        public Guid CategoryId { get; set; }

        public Guid ItemId { get; set; }
    }
}