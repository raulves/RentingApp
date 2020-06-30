using System;

namespace PublicApi.DTO.v1.AppUserCompanyDTOs
{
    public class AppUserCompanyEditDTO
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
    }
}