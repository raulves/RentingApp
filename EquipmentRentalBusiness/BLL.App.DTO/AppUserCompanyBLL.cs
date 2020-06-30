using System;
using System.Text.Json.Serialization;
using BLL.App.DTO.Identity;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace BLL.App.DTO
{
    public class AppUserCompanyBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        [JsonIgnore]
        public AppUserBLL? AppUser { get; set; }
        
        public Guid CompanyId { get; set; } = default!;
        [JsonIgnore]
        public CompanyBLL? Company { get; set; }
    }
    
}