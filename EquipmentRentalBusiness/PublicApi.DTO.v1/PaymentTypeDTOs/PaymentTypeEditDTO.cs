using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.PaymentTypeDTOs
{
    public class PaymentTypeEditDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)]
        public string Description { get; set; } = default!;
    }
}