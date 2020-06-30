using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.ItemDTOs
{
    public class ItemCreateDTO
    {
        [MaxLength(100)]
        public string Description { get; set; } = default!;
        
        [MaxLength(64)]
        public string Brand { get; set; } = default!;
        
        [MaxLength(64)]
        public string Model { get; set; } = default!;
        
        public Guid LocationId { get; set; }
    }
}