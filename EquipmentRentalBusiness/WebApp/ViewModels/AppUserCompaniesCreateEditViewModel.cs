#pragma warning disable 1591
using System;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class AppUserCompanyCreateEditViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        
        public Guid AppUserId { get; set; }
        
        public SelectList? CompaniesSelectList { get; set; }
    }
}