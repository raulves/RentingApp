using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.PaymentTypeDTOs
{
    public class PaymentTypeCreateDTO
    {
        [MaxLength(64)]
        public string Description { get; set; } = default!;
    }
}