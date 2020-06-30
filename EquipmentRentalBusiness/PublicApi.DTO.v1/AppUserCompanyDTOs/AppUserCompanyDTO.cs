using System;

using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace PublicApi.DTO.v1.AppUserCompanyDTOs
{
    public class AppUserCompanyDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        
        public Guid AppUserId { get; set; }
    }
}