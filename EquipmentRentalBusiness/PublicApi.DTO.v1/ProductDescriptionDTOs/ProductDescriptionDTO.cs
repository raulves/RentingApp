using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.ProductDescriptionDTOs
{
    public class ProductDescriptionDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)]
        public string Description { get; set; } = default!;
    }
}