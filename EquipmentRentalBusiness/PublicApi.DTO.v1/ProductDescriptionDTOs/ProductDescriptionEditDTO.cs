using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.ProductDescriptionDTOs
{
    public class ProductDescriptionEditDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)]
        public string Description { get; set; } = default!;
    }
}