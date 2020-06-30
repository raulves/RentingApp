using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.CompanyDTOs
{
    public class CompanyDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
        [MaxLength(64)] public string CompanyName { get; set; } = default!;

        [MaxLength(64)] public string RegisterCode { get; set; } = default!;
        
        [MaxLength(64)]
        public string? VatNumber { get; set; }
        
        [MaxLength(64)]
        public string? Email { get; set; }
        
        [MaxLength(64)]
        public string? Phone { get; set; }
        
        public Guid LocationId { get; set; }
    }
}