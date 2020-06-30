using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class LocationDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public string AddressLine { get; set; } = default!;
        
        public string City { get; set; } = default!;
        
        public string County { get; set; } = default!;
        
        public string Country { get; set; } = default!;

        [JsonIgnore]
        public ICollection<ItemDAL>? Items { get; set; }
        [JsonIgnore]
        public ICollection<CompanyDAL>? Companies { get; set; }
        [JsonIgnore]
        public ICollection<AppUserDAL>? AppUsers { get; set; }
    }
    
}