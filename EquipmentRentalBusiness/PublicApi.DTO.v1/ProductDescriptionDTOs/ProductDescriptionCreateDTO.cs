using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.ProductDescriptionDTOs
{
    public class ProductDescriptionCreateDTO
    {
        [MaxLength(64)]
        public string Description { get; set; } = default!;
    }
}