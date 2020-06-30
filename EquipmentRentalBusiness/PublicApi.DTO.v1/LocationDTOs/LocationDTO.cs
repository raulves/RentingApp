using System;
using System.ComponentModel.DataAnnotations;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.LocationDTOs
{
    public class LocationDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        
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