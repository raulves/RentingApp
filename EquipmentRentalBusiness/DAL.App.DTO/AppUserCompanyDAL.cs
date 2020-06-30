using System;
using System.Text.Json.Serialization;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class AppUserCompanyDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserDAL? AppUser { get; set; }
        
        public Guid CompanyId { get; set; } = default!;
        [JsonIgnore]
        public CompanyDAL? Company { get; set; }
    }
    
    
}