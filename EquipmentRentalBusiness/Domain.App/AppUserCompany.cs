using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.Raul.Vesinurm.Domain.Base;

namespace Domain.App
{

    public class AppUserCompany : DomainEntityIdMetadataUser<AppUser>
    {
        [Display(Name = nameof(CompanyId), ResourceType = typeof(Resources.Domain.AppUserCompany.AppUserCompany))]
        public Guid CompanyId { get; set; }
        
        public Company? Company { get; set; }
        
    }
    
}