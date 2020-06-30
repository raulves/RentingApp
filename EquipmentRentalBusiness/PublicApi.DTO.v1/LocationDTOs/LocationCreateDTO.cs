using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.LocationDTOs
{
    public class LocationCreateDTO
    {
        [MaxLength(64)] 
        public string AddressLine { get; set; } = default!;
        
        [MaxLength(64)]
        public string City { get; set; } = default!;
        
        [MaxLength(64)]
        public string County { get; set; } = default!;
        
        [MaxLength(64)]
        public string Country { get; set; } = default!;
    }
}