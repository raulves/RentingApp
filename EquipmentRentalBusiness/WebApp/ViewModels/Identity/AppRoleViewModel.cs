#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.Raul.Vesinurm.Contracts.Domain;

namespace WebApp.ViewModels.Identity
{
    public class AppRoleViewModel : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        [Display(Name = nameof(DisplayName), ResourceType = typeof(Resources.Domain.Role.Role))]
        public string DisplayName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Role.Role))]
        public string Name { get; set; } = default!;
    }
}