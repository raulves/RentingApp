#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;
using Domain.App.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class AppUserToRoleDeleteViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }

        public AppUser AppUser { get; set; } = default!;

        public Guid RoleId { get; set; }
        
        [Display(Name = nameof(Roles), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public SelectList? Roles { get; set; }
        
    }
}