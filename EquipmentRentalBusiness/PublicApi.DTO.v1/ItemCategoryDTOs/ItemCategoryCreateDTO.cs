using System;

namespace PublicApi.DTO.v1.ItemCategoryDTOs
{
    public class ItemCategoryCreateDTO
    {
        public Guid CategoryId { get; set; }

        public Guid ItemId { get; set; }
    }
}