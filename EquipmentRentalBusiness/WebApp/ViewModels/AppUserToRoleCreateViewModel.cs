#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class AppUserToRoleCreateViewModel
    {
        [Display(Name = nameof(AppUserId), ResourceType = typeof(Resources.Domain.AppUser.AppUser))]
        public Guid AppUserId { get; set; }
        
        [Display(Name = nameof(RoleId), ResourceType = typeof(Resources.Domain.Role.Role))]
        public Guid RoleId { get; set; }

        public SelectList? AppUserSelectList { get; set; }
        public SelectList? RoleSelectList { get; set; }
    }
}