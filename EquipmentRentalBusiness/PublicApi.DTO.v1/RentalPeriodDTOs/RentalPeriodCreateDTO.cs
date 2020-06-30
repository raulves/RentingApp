using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.RentalPeriodDTOs
{
    public class RentalPeriodCreateDTO
    {
        [MaxLength(64)]
        public string Description { get; set; } = default!;
        public int PeriodStart { get; set; }
        public int PeriodEnd { get; set; }
    }
}