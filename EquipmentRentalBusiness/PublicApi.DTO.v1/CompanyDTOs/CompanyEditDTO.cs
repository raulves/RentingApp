using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.CompanyDTOs
{
    public class CompanyEditDTO
    {
        public Guid Id { get; set; }
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