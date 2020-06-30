using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class LocationBLL : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUserBLL? AppUser { get; set; }
        
        public string AddressLine { get; set; } = default!;
        
        public string City { get; set; } = default!;
        
        public string County { get; set; } = default!;
        
        public string Country { get; set; } = default!;

        public ICollection<ItemBLL>? Items { get; set; }
        public ICollection<CompanyBLL>? Companies { get; set; }
        public ICollection<AppUserBLL>? AppUsers { get; set; }
    }
    
}