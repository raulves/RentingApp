using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.ItemDTOs
{
    public class ItemDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; } = default!;
        
        [MaxLength(64)]
        public string Brand { get; set; } = default!;
        
        [MaxLength(64)]
        public string Model { get; set; } = default!;
        
        public Guid LocationId { get; set; }
    }
}