using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{
    public class Location : DomainEntityIdMetadataUser<AppUser>
    {
        [MinLength(1)]
        [MaxLength(64)]
        public string AddressLine { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        public string City { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        public string County { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        public string Country { get; set; } = default!;

        public ICollection<Item>? Items { get; set; }
        public ICollection<Company>? Companies { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }
    }
    
}